namespace TH.AddressMS.Core
{
    public class Country : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Address> CountryAddress { get; set; } = new List<Address>();
    }
}