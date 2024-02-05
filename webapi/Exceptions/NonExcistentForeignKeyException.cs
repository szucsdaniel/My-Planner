namespace webapi.Exceptions
{
    /// <summary>
    /// This exception is thrown whenever a foreign key that must be set was either not provided or there is no entity with the Id
    /// </summary>
    public class NonExcistentForeignKeyException : Exception
    {
        public NonExcistentForeignKeyException(string entityType)
            : base($"The given foreign key of {ExceptionMessageFunctions.GetEntityTypeName(entityType)} does not exist") { }
    }
}
