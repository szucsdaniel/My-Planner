using webapi.Models;

namespace webapi.Mapping.PostDTOs
{
    public class SubTaskPostDTO : BasePostDTO
    {
        public int BranchId { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }

    }
}
