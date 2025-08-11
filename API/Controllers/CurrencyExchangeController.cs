using Application.CurrencyExchange;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyExchangeController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpPost]
        public async Task<IActionResult> AddRate([FromBody] AddCurrencyExchangeRateCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id });
        }
    }
}

