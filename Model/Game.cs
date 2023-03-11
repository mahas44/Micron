using Models.Attributes;

namespace Models
{
    public class Game
    {
        [StringData(Min = 3)]
        public string Name { get; set; }
        [StringData(Max = 250)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Platform Platform { get; set; }
        public string Publisher { get; set; }
        public float Storage { get; set; }
        public UnitOfStorage UnitOfStorage { get; set; }
        public DateTime ReleaseDate { get; set; }
        [StringData(Min = 3, Max = 3)]
        public string Currency { get; set; }

    }

    public enum Platform
    {
        PC = 0,
        PS = 1,
        XBOX = 2,
        MOBILE = 3
    }

    public enum UnitOfStorage
    {
        KB = 0,
        MB = 1,
        GB = 2
    }
}
