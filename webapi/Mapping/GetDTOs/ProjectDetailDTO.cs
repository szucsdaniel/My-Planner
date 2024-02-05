namespace webapi.Mapping.GetDTOs
{
    public class ProjectDetailDTO : BaseGetDTO
    {
        public ICollection<BranchGetListDTO> Branches { get; set; }
        public ICollection<AssigneeGetListDTO> Assignees { get; set; }
        public string? Description { get; set; }
    }
}
