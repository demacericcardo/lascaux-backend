using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Schedule: BaseEntity
    {
        [Key, ForeignKey(nameof(Film))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        override public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        [ForeignKey(nameof(oneToMany))]
        public int FK_Screen { get; set; }
        public virtual Screen oneToMany { get; set; } = null!;
    }
}
