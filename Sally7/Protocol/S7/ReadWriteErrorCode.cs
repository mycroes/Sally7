namespace Sally7.Protocol.S7
{
    /// <summary>
    /// Defines error codes that can be returned for read and write operations on a S7 PLC.
    /// These error codes are defined in the S7 protocol specification and can be used to determine the reason for a failed read or write operation.
    /// </summary>
    public enum ReadWriteErrorCode : byte
    {
        /// <summary>
        /// Indicates that the error code is reserved and should not be used.
        /// </summary>
        Reserved = 0x00,

        /// <summary>
        /// Indicates that a hardware fault occurred during the read or write operation.
        /// </summary>
        HardwareFault = 0x01,

        /// <summary>
        /// Indicates that the requested operation is not allowed on the accessed object.
        /// </summary>
        AccessingObjectNotAllowed = 0x03,

        /// <summary>
        /// Indicates that the address specified in the read or write request is out of range for the accessed object.
        /// </summary>
        AddressOutOfRange = 0x05,

        /// <summary>
        /// Indicates that the data type specified in the read or write request is not supported by the accessed object.
        /// </summary>
        DataTypeNotSupported = 0x06,

        /// <summary>
        /// Indicates that the data type specified in the read or write request is inconsistent with the data type of the accessed object.
        /// </summary>
        DataTypeInconsistent = 0x07,

        /// <summary>
        /// Indicates that the request object does not exist.
        /// </summary>
        ObjectDoesNotExist = 0x0a,

        /// <summary>
        /// Indicates that the read or write operation was successful and no error occurred.
        /// </summary>
        Success = 0xff
    }
}