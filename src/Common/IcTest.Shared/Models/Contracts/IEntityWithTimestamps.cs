namespace IcTest.Shared.Models.Contracts
{
    public interface IEntityWithTimestamps : IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
