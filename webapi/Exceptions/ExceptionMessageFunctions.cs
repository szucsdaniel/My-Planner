using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Metadata.Ecma335;

namespace webapi.Exceptions
{
    public static class ExceptionMessageFunctions
    {
        // This function extracts the Property's name that the exception is thrown for.
        public static string GetPropertyName(EntityEntry? entry)
        {
            var keyProperty = entry?.Properties.Where(p => p.Metadata.IsKey()).ToList().FirstOrDefault();
            return keyProperty?.Metadata.Name.Split(".").Last() ?? "Unknown Property Name";
        }
        // This function returns with the exact type name without the folder names.
        public static string GetEntityTypeName(string entityType)
        {
            return entityType.Split('.').Last();
        }
    }
}
