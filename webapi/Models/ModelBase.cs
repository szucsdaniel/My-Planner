using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public abstract class ModelBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        // This method is responsible for Validating the properties of an Entity before changing the database.
        public virtual void Validate()
        {
            var validationContext = new ValidationContext(this, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, validateAllProperties: true);

            if (validationResults.Count > 0)
            {
                throw new ValidationException(validationResults[0].ErrorMessage);
            }
        }
    }
}
