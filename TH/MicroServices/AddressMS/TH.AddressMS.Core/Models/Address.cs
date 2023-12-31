namespace TH.AddressMS.Core
{
    public class Address : BaseEntity
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string CountryId { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
    }
}