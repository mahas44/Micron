namespace Models.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class DateData : Attribute
    {
        public DateData()
        {
        }
        public int MinYear { get; set; }

    }
}
