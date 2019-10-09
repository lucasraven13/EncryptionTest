using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Models;
using APIGateway.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionController : ControllerBase
    {
        private readonly IEncryptionService _encryptionService;

        public EncryptionController(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        [HttpPost("encrypt")]
        public async Task<IActionResult> Encrypt(EncryptionModel model)
        {
            var result = new ExpandoObject();
            result.TryAdd("data", await _encryptionService.EncryptAsync(model));
            return Ok(result);
        }

        [HttpPost("decrypt")]
        public async Task<IActionResult> Decrypt(DecryptionModel model)
        {
            var result = new ExpandoObject();
            result.TryAdd("secret", await _encryptionService.DecryptAsync(model));
            return Ok(result);
        }
    }
}
