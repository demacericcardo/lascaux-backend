namespace BL.Dtos
{
    public class ScheduleInputDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FK_Screen { get; set; }
        public int FK_Film { get; set; }
    }

    public class ScheduleOutputDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required ScreenOutputDto Screen { get; set; }
        public required FilmOutputDto Film { get; set; }
    }
}
