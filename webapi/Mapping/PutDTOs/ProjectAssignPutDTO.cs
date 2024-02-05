namespace webapi.Mapping.PutDTOs
{
    public class ProjectAssignPutDTO
    {
        public int Id { get; set; }
        public ICollection<AssignPersonPutDTO> Assignees { get; set; } = new List<AssignPersonPutDTO>();
    }
}
