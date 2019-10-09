using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EncryptionService.Models;
using EncryptionService.Services.Interfaces;

namespace EncryptionService.Services.Implementations
{
    public class EncryptingService : IEncryptionService
    {
        private readonly IEncryptionKeyService _encryptionKeyService;
        public EncryptingService(IEncryptionKeyService encryptionKeyService)
        {
            _encryptionKeyService = encryptionKeyService;
        }

        public async Task<string> DecryptAsync(DecryptionModel model)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(model.Data);

            using (Aes aes = Aes.Create())
            {
                try
                {
                    aes.Key = Encoding.UTF8.GetBytes(_encryptionKeyService.GetKey());
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return await streamReader.ReadToEndAsync();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Message could not be decrypted with current encryption key");
                }
            }
        }

        public async Task<string> EncryptAsync(EncryptionModel model)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKeyService.GetKey());
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            await streamWriter.WriteAsync(model.Secret);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
    }
}