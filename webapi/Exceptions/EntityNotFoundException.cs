namespace webapi.Exceptions
{
    /// <summary>
    /// This Exception is thrown whenever an Entity cannot be found in the database. 
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityType)
            : base($"{ExceptionMessageFunctions.GetEntityTypeName(entityType)} not found"){}

    }
}
