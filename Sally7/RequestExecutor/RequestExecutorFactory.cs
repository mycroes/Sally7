namespace Sally7.RequestExecutor
{
    /// <summary>
    /// Defines the factory method to create a <see cref="IRequestExecutor"/> for the given connection.
    /// </summary>
    /// <param name="connection">The connection to create an executor for.</param>
    /// <returns>An instance of a class implementing the <see cref="IRequestExecutor"/> interface.</returns>
    public delegate IRequestExecutor RequestExecutorFactory(S7Connection connection);
}