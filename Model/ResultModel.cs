namespace Models
{
    public class ResultModel<T>
    {
        public List<CustomException> Exceptions { get; set; }
        public T Value { get; set; }
    }
}
