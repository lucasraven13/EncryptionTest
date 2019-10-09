using System.ComponentModel.DataAnnotations;

namespace EncryptionService.Models
{
    public class EncryptionModel
    {
        [Required]
        public string Secret { get; set; }
    }
}