using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eShopApi.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int AddressId { get; set; }

        [ForeignKey("UserDetails")]
        public int UserId { get; set; }
        public string AddressInfo { get; set; }
        public string City { get; set; }
        public string UserState { get; set; }
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode should be a 6-digit number.")]
        public string Pincode { get; set; }
    }
}
