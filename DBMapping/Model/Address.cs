using System.ComponentModel.DataAnnotations.Schema;

namespace DBMapping.Model
{
    public class Address
    {
       [ForeignKey("Employee")]
        public Guid AddressId { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
