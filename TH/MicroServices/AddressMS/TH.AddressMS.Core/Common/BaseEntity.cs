namespace TH.AddressMS.Core
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; } = true;
    }
}