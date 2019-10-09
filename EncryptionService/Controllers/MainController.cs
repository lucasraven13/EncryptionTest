using System.Threading.Tasks;
using EncryptionService.Models;
using EncryptionService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionService.Controllers
{
    [Route("api")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IEncryptionKeyService _encryptionKeyService;
        private readonly IEncryptionService _encryptionService;
        public MainController(IEncryptionKeyService encryptionKeyService, IEncryptionService encryptionService)
        {
            _encryptionKeyService = encryptionKeyService;
            _encryptionService = encryptionService;
        }

        [HttpPost("encrypt")]
        public async Task<IActionResult> Encrypt([FromBody] EncryptionModel model)
        {
            return Ok(await _encryptionService.EncryptAsync(model));
        }

        [HttpPost("decrypt")]
        public async Task<IActionResult> Decrypt([FromBody] DecryptionModel model)
        {
            return Ok(await _encryptionService.DecryptAsync(model));
        }

        [HttpPut("key")]
        public IActionResult RotateKey()
        {
            _encryptionKeyService.RotateKey();
            return StatusCode(202);
        }
    }
}
