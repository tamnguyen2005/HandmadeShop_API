namespace HandmadeShop.Domain.Commons
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedTime { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}