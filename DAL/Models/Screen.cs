using DAL.Models.Base;

namespace DAL.Models
{
    public class Screen: BaseEntity
    {
        public bool HasIMAX { get; set; }
        public int Capacity { get; set; }
    }
}
