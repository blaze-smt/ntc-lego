using System.Xml.Serialization;

namespace GamerParadise.Models
{
    [XmlRoot("AddressValidateResponse")]
    public class UspsAddressValidateResponse
    {
        public UspsAddress Address { get; set; }
    }
}