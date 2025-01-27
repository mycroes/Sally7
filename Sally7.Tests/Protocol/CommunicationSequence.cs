using System.Buffers;
using System.Net;
using System.Net.Sockets;
using Sally7.Protocol.Cotp;
using Sally7.Protocol.S7;
using Xunit.Abstractions;

namespace Sally7.Tests.Protocol;

internal class CommunicationSequence
{
    private readonly List<(Fragment[], Fragment[])> _sequence = new();

    private readonly ITestOutputHelper _output;

    public static Fragment Pdu1 { get; } = nameof(Pdu1);
    public static Fragment Pdu2 { get; } = nameof(Pdu2);

    public CommunicationSequence(ITestOutputHelper output)
    {
        _output = output;
    }

    public class Fragment
    {
        public byte? Value { get; private init; }
        public string? Key { get; private init; }

        private Fragment()
        {
        }

        public static implicit operator Fragment(byte value) => new() { Value = value };
        public static implicit operator Fragment(string value) => new() { Key = value };

        public static IEnumerable<Fragment> FromBytes(IEnumerable<byte> bytes) => bytes.Select(x => (Fragment)x);
    }

    public CommunicationSequence AddCall(Fragment[] request, Fragment[] response)
    {
        _sequence.Add((request, response));

        return this;
    }

    public CommunicationSequence AddConnectRequest(PduSizeParameter.PduSize pduSize, Tsap sourceTsap, Tsap destinationTsap)
    {
        return AddCall(new Fragment[]
        {
            // TPKT
            3, // Version
            0, // Reserved
            0, 22, // Length

            // CR
            17, // Number of bytes following
            0b1110_0000, // CR / Credit
            0, 0, // Destination reference, unused
            0, 0, // Source reference, unused
            0, // Class / Option

            // PDU Size parameter
            0b1100_0000, // Parameter code
            1, // Parameter length
            (byte) pduSize, // 1024 byte PDU (2 ^ 10)

            // Source TSAP
            0b1100_0001, // Parameter code
            2, // Parameter length
            sourceTsap.Channel, // Channel
            sourceTsap.Position, // Position

            // Destination TSAP
            0b1100_0010, // Parameter code
            2, // Parameter length
            destinationTsap.Channel, // Channel
            destinationTsap.Position, // Position
        }, new Fragment[]
        {
            // TPKT
            3, // Version
            0, // Reserved
            0, 11, // Length

            // CC
            6, // Length
            0b1101_0000, // CC / Credit
            0, 0, // Destination reference
            0, 0, // Source reference
            0, // Class / Option
        });
    }

    public CommunicationSequence AddCommunicationSetup()
    {
        return AddCall(new Fragment[]
        {
            // TPKT
            3, // Version
            0, // Reserved
            0, 25, // Length

            // Data header
            2, // Length
            0b1111_0000, // Data identifier
            0b1_000_0000, // PDU number and end of transmission

            // S7 header
            0x32, // Protocol ID
            0x01, // Message type job request
            0, 0, // Reserved
            Pdu1, Pdu2, // PDU reference
            0, 8, // Parameter length (Communication Setup)
            0, 0, // Data length

            // Communication Setup
            0xf0, // Function code
            0, // Reserved
            0, 10, // Max AMQ caller
            0, 10, // Max AMQ callee
            3, 192, // PDU size (960)
        }, new Fragment[]
        {
            // TPKT
            3, // Version
            0, // Reserved
            0, 27, // Length

            // Data header
            2, // Length
            0b1111_0000, // Data identifier
            0b1_000_0000, // PDU number and end of transmission

            // S7 header
            0x32, // Protocol ID
            0x03, // Message type ack data
            0, 0, // Reserved
            Pdu1, Pdu2, // PDU reference
            0, 8, // Parameter length (Communication Setup)
            0, 0, // Data length
            0, // Error class
            0, // Error code

            // Communication Setup
            0xf0, // Function code
            0, // Reserved
            0, 3, // Max AMQ caller
            0, 3, // Max AMQ callee
            3, 192, // PDU size (960)
        });
    }

    public CommunicationSequence AddRead(Area area, int dbNumber, int address, int length, TransportSize transportSize,
        VariableType variableType, byte[] data)
    {
        var dataLength = 4 + data.Length;

        return AddCall(new Fragment[]
        {
            // TPKT
            3, // Version
            0, // Reserved
            0, 31, // Length

            // Data header
            2, // Length
            0b1111_0000, // Data identifier
            0b1_000_0000, // PDU number and end of transmission

            // S7 header
            0x32, // Protocol ID
            0x01, // Message type job request
            0, 0, // Reserved
            Pdu1, Pdu2, // PDU reference
            0, 14, // Parameter length (Read request)
            0, 0, // Data length

            // Read request
            0x04, // Function code
            1, // Number of items

            // Request item
            0x12, // Spec
            10, // Length of request item
            0x10, // AddressingMode S7 any
            (byte) variableType, // Variable type
            (byte) (length >> 8 & 0xff), // Length, upper byte
            (byte) (length & 0xff), // Length, lower byte
            (byte) (dbNumber >> 8 & 0xff), // DB number, upper byte
            (byte) (dbNumber & 0xff), // DB number, lower byte
            (byte) area,
            (byte) (address >> 16 & 0xff), // Address, upper byte
            (byte) (address >> 8 & 0xff), // Address, middle byte
            (byte) (address & 0xff), // Address, lower byte
        }, new Fragment[]
        {
            // TPKT
            3, // Version
            0, // Reserved
            0, 27, // Length

            // Data header
            2, // Length
            0b1111_0000, // Data identifier
            0b1_000_0000, // PDU number and end of transmission

            // S7 header
            0x32, // Protocol ID
            0x03, // Message type ack data
            0, 0, // Reserved
            Pdu1, Pdu2, // PDU reference
            0, 2, // Parameter length (Read request)
            (byte) (dataLength >> 8 & 0xff), (byte) (dataLength & 0xff), // Data length
            0, // Error class
            0, // Error code

            // Read response
            0x04, // Function code
            1, // Number of items

            // DataItem
            0xff, // ErrorCode
            (byte) transportSize, // Transport size
            (byte) (data.Length >> 5 & 0xff), // Data length, upper byte, in bits
            (byte) (data.Length << 3 & 0xff), // Data length, lower byte, in bits
        }.Concat(Fragment.FromBytes(data)).ToArray());
    }

    public CommunicationSequence AddWrite(Area area, int dbNumber, int address, int length, TransportSize transportSize,
        VariableType variableType, byte[] data)
    {
        var dataLength = 4 + data.Length;

        return AddCall(new Fragment[]
        {
            // TPKT
            3, // Version
            0, // Reserved
            0, 37, // Length

            // Data header
            2, // Length
            0b1111_0000, // Data identifier
            0b1_000_0000, // PDU number and end of transmission

            // S7 header
            0x32, // Protocol ID
            0x01, // Message type job request
            0, 0, // Reserved
            Pdu1, Pdu2, // PDU reference
            0, 14, // Parameter length (Write request)
            (byte)(dataLength >> 8 & 0xff), (byte)(dataLength & 0xff), // Data length

            // Write request
            0x05, // Function code
            1, // Number of items

            // Request item
            0x12, // Spec
            10, // Length of request item
            0x10, // AddressingMode S7 any
            (byte) variableType, // Variable type
            (byte) (length >> 8 & 0xff), // Length, upper byte
            (byte) (length & 0xff), // Length, lower byte
            (byte) (dbNumber >> 8 & 0xff), // DB number, upper byte
            (byte) (dbNumber & 0xff), // DB number, lower byte
            (byte) area,
            (byte) (address >> 16 & 0xff), // Address, upper byte
            (byte) (address >> 8 & 0xff), // Address, middle byte
            (byte) (address & 0xff), // Address, lower byte,

            // Data
            0, // ErrorCode
            (byte) transportSize, // Transport size
            (byte) (data.Length >> 5 & 0xff),
            (byte) (data.Length << 3 & 0xff),
        }.Concat(Fragment.FromBytes(data)).ToArray(), new Fragment[]
        {
            // TPKT
            3, // Version
            0, // Reserved
            0, 22, // Length

            // Data header
            2, // Length
            0b1111_0000, // Data identifier
            0b1_000_0000, // PDU number and end of transmission

            // S7 header
            0x32, // Protocol ID
            0x03, // Message type ack data
            0, 0, // Reserved
            Pdu1, Pdu2, // PDU reference
            0, 2, // Parameter length (Write response)
            0, 1, // Data length
            0, // Error class
            0, // Error code

            // Write response
            0x05, // Function code
            1, // Number of items

            // Result code per item
            0xff, // ErrorCode
        }.Concat(Fragment.FromBytes(data)).ToArray());
    }

    public Task Serve(out int port)
    {
        var socket = CreateBoundListenSocket(out port);
        socket.Listen();

        async Task Impl()
        {
            var socketIn = await socket.AcceptAsync();

            var buffer = ArrayPool<byte>.Shared.Rent(1024);
            try
            {
                foreach (var (request, response) in _sequence)
                {
                    var bytesReceived = await socketIn.ReceiveAsync(buffer, SocketFlags.None);

                    var received = buffer.Take(bytesReceived).ToArray();
                    _output.WriteLine($"=> {BitConverter.ToString(received)}");

                    var captures = new Dictionary<string, byte>();

                    for (var i = 0; i < Math.Min(request.Length, received.Length); i++)
                    {
                        if (request[i].Key is { } key)
                        {
                            captures.Add(key, received[i]);
                        }
                        else if (request[i].Value != received[i])
                        {
                            throw new Exception(
                                $"Value '{received[i]}' at index {i} of received data does not match expected value '{request[i].Value}'.");
                        }
                    }

                    var res = response.Select((f, i) =>
                    {
                        if (f.Key is not { } key) return f.Value!.Value;

                        if (captures.TryGetValue(key, out var capture)) return capture;

                        throw new Exception(
                            $"Placeholder '{key}' at index {i} of response was not captured from the request.");
                    }).ToArray();

                    _output.WriteLine($"<= {BitConverter.ToString(res)}");
                    socketIn.Send(res);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }

            socketIn.Close();
        }

        return Impl();
    }

    private static Socket CreateBoundListenSocket(out int port)
    {
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var endpoint = new IPEndPoint(IPAddress.Loopback, 0);

        socket.Bind(endpoint);

        var localEndpoint = (IPEndPoint)socket.LocalEndPoint!;
        port = localEndpoint.Port;

        return socket;
    }
}