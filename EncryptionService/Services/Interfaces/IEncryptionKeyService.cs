namespace EncryptionService.Services.Interfaces
{
    public interface IEncryptionKeyService
    {
        string GetKey();
        void RotateKey();
    }
}