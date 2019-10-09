using System.ComponentModel.DataAnnotations;

namespace APIGateway.Models
{
    public class EncryptionModel
    {
        [Required]
        public string Secret { get; set; }
    }
}