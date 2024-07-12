namespace BL.Dtos
{
    public class ScheduleInputDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FK_Screen { get; set; }
    }

    public class ScheduleOutputDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required ScreenOutputNoRefDto Screen { get; set; }
    }
}
