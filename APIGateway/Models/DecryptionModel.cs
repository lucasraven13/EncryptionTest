using System.ComponentModel.DataAnnotations;

namespace APIGateway.Models
{
    public class DecryptionModel
    {
        [Required]
        public string Data { get; set; }
    }
}