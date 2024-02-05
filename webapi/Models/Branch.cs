using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Branch: ModelBase
    {
        // The project that it belongs to. The ProjectId will be the foreign key in the table.
        [Required(ErrorMessage = "Project ID must have a value")]
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        // The Subtasks that are part of the Branch. 
        public ICollection<SubTask> Subtasks { get; set; } = new List<SubTask>();
    }
}
