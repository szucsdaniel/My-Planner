using webapi.Models;

namespace webapi.Mapping.PutDTOs
{
    public class SubTaskPutDTO : BasePutDTO
    {
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }
    }
}
