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
        public ScheduleOutputDto? Schedule { get; set; }
    }
}
