using System;
using System.Security.Cryptography;
using System.Text;
using EncryptionService.Services.Interfaces;

namespace EncryptionService.Services.Implementations
{
    public class EncryptionKeyService : IEncryptionKeyService
    {
        private string _encryptionKey;

        public EncryptionKeyService()
        {
            RotateKey();
        }

        public string GetKey()
        {
            return _encryptionKey;
        }

        public void RotateKey()
        {
            int length = 16;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            _encryptionKey = res.ToString();
        }
    }
}