using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class SubTask: ModelBase
    {
        [Required(ErrorMessage ="Deadline is required")]
        public DateTime Deadline { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public Status Status { get; set; }
        //The Branch that the SubTask belongs to. The BranchId will be the foreign key in the table.
        [Required(ErrorMessage = "Branch ID must have a value")]
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        // The Assignees working on the SubTask
        public List<Assignee> Assignees { get; set; } = new List<Assignee>();

    }
    public enum Status
    {
        WAITING, IN_PROGRESS, FINISHED
    }
}
