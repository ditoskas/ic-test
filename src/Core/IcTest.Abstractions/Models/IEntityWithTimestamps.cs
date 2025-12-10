namespace IcTest.Abstractions.Models
{
    public interface IEntityWithTimestamps : IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
