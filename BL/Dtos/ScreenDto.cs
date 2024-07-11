namespace BL.Dtos
{
    public class ScreenInputDto
    {
        public bool HasIMAX { get; set; }
        public int Capacity { get; set; }
    }

    public class ScreenOutputDto
    {
        public int Id { get; set; }
        public bool HasIMAX { get; set; }
        public int Capacity { get; set; }
    }
}
