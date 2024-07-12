using DAL.Models.Base;

namespace DAL.Models
{
    public class Screen: BaseEntity
    {
        public required string Name { get; set; }
        public bool HasIMAX { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new HashSet<Schedule>();
    }
}
