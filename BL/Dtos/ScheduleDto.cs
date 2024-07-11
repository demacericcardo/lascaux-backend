namespace BL.Dtos
{
    public class ScheduleInputDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FK_Sala { get; set; }
        public int FK_Film { get; set; }
    }
}
