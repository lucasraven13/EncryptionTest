using System.Threading.Tasks;
using APIGateway.Models;

namespace APIGateway.Services.Interfaces
{
    public interface IEncryptionService
    {
        Task<string> EncryptAsync(EncryptionModel model);
        Task<string> DecryptAsync(DecryptionModel model);
    }
}