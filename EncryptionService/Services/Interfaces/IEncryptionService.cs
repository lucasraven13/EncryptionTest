using System.Threading.Tasks;
using EncryptionService.Models;

namespace EncryptionService.Services.Interfaces
{
    public interface IEncryptionService
    {
        Task<string> EncryptAsync(EncryptionModel model);
        Task<string> DecryptAsync(DecryptionModel model);
    }
}