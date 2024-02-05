namespace webapi.Models
{
    // Someone who participates in the Project or works on a SubTask
    public class Assignee: ModelBase
    {
        public List<Project> Projects { get; set; } = new List<Project>();

        public List<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }
}
