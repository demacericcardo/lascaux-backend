using DAL.Enums;

namespace BL.Dtos
{
    public class FilmInputDto
    {
        public required string Title { get; set; }
        public required string Director { get; set; }
        public string? Description { get; set; }
        public FilmGenre Genre { get; set; }
        public int MinuteLenght { get; set; }
    }

    public class FilmOutputDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Director { get; set; }
        public string? Description { get; set; }
        public FilmGenre Genre { get; set; }
        public int MinuteLenght { get; set; }
        public IEnumerable<FilmSchedule> Schedules { get; set; } = [];
    }

    public class FilmSchedule
    {
        public int ScreenId { get; set; }
        public required string ScreenName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
