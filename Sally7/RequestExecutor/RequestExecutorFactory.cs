namespace Sally7.RequestExecutor
{
    /// <summary>
    /// Defines the factory method to create a <see cref="IRequestExecutor"/> for the given connection.
    /// </summary>
    /// <param name="connection">The connection to create an executor for.</param>
    /// <returns>An instance of a class implementing the <see cref="IRequestExecutor"/> interface.</returns>
    public delegate IRequestExecutor RequestExecutorFactory(S7Connection connection);

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
    public delegate IRequestExecutorValueTask RequestExecutorFactoryValueTask(S7ConnectionValueTask connection);
#endif
}
