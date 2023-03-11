namespace Models.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class StringData : Attribute
    {
        public StringData()
        {
        }

        public int Max { get; set; }
        public int Min { get; set; }
    }
}
