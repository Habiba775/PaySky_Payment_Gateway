using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using API.Models;
using Domain.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService _merchantService;

        public MerchantController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMerchant([FromBody] CreateMerchantRequest request)
        {
            var merchant = await _merchantService.CreateMerchantAsync(request.Name, request.Email);
            return Ok(merchant);
        }
    }


}