using webapi.Models;

namespace webapi.Mapping.GetDTOs
{
    public class SubTaskGetListDTO : BaseGetDTO
    {
        public int BranchId { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }
    }
}
