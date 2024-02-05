using webapi.Models;

namespace webapi.Mapping.GetDTOs
{
    public class SubTaskGetDetailDTO : BaseGetDTO
    {
        public ICollection<AssigneeGetListDTO> Assignees { get; set; }
        public int BranchId { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }
    }
}
