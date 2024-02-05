namespace webapi.Mapping.PutDTOs
{
    public class SubTaskAssignPutDTO
    {
        public int Id { get; set; }
        public ICollection<AssignPersonPutDTO> Assignees { get; set; } = new List<AssignPersonPutDTO>();
    }
}
