using DAL.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Schedule: BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        [ForeignKey(nameof(Screen))]
        public int FK_Screen { get; set; }
        public virtual Screen Screen { get; set; } = null!;


        [ForeignKey(nameof(Film))]
        public int FK_Film { get; set; }
        public virtual Film Film { get; set; } = null!;
    }
}
