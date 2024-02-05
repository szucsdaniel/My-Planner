using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Project: ModelBase
    {
        public string? Description { get; set; }
        // The branches that are part of the project
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
        // The Assignees working on the Project
        public List<Assignee> Assignees { get; set; } = new List<Assignee>();


    }
}
