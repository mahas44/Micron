namespace Models
{
    public class BaseModel
    {
        public DateTime PostDate { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public Guid RequestId { get; set; }
        public object? Value { get; set; }
    }
}