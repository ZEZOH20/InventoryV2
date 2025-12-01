using System.ComponentModel.DataAnnotations;

namespace InventoryV2.Models
{
    public class Person: AuditableEntity
    {
        [MaxLength(100)] 
        public string Name { get; set; } = null!;

        [RegularExpression(@"^01[0125]\d{8}$", ErrorMessage = "Insert a valid Phone Number")]
        public int Phone { get; set; }
        public string? Fax { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Mail is required")]
        public  string? Mail { get; set; }
        public string? Domain { get; set; }
    }
}
