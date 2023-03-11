namespace Models
{
    public class CustomException
    {
        public bool IsSuccesful { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorType ErrorType { get; set; }
        public string Source { get; set; }
    }

    public enum ErrorType
    {
        Info,
        Warning,
        Error,
    }
}
