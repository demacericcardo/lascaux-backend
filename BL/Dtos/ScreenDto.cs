namespace BL.Dtos
{
    public class ScreenInputDto
    {
        public required string Name { get; set; }
        public bool HasIMAX { get; set; }
        public int Capacity { get; set; }
    }

    public class ScreenOutputDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool HasIMAX { get; set; }
        public int Capacity { get; set; }
        public IEnumerable<ScheduleOutputDto> Schedules { get; set; } = [];
    }

    public class ScreenOutputNoRefDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool HasIMAX { get; set; }
        public int Capacity { get; set; }
    }
}
