namespace webapi.Mapping.GetDTOs
{
    public class BranchGetDetailDTO : BaseGetDTO
    {
        public int ProjectId { get; set; }
        public ICollection<SubTaskGetListDTO> Subtasks { get; set; }
    }
}
