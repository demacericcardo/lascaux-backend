namespace BL.Responses
{
    public class ServiceResponse<T> : ServiceResponse where T : class
    {
        public T? Value { get; set; }
    }

    public class ServiceResponseStruct<T> : ServiceResponse where T : struct
    {
        public T? Value { get; set; }
    }

    public class ServiceResponse
    {
        public string? ErrorMessage { get; set; }
        public bool IsSuccessful { get; set; } = true;
    }
}
