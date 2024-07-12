using DAL.Enums;
using DAL.Models.Base;

namespace DAL.Models
{
    public class Film : BaseEntity
    {
        public required string Title { get; set; }
        public required string Director { get; set; }
        public string? Description { get; set; }
        public FilmGenre Genre { get; set; }
        public int MinuteLenght { get; set; }

        public virtual Schedule? oneToOne { get; set; }
    }
}
