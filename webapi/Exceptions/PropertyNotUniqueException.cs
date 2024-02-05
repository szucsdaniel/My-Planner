using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace webapi.Exceptions
{
    /// <summary>
    /// This exception is thrown when a records property is not unique despite of the constraint.
    /// </summary>
    public class PropertyNotUniqueException : Exception 
    {

        public PropertyNotUniqueException(EntityEntry? entry, string entityType)
            : base($"The given {ExceptionMessageFunctions.GetPropertyName(entry)} of {ExceptionMessageFunctions.GetEntityTypeName(entityType)} is not unique"){ }


        
    }
}
