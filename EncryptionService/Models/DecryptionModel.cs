using System.ComponentModel.DataAnnotations;

namespace EncryptionService.Models
{
    public class DecryptionModel
    {
        [Required]
        public string Data { get; set; }
    }
}