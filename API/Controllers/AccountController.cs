using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Accounts.Queries;
using Application.Accounts.Queries.Application.Accounts.Queries;
using Application.Accounts.Commands.CreateAccount;
using Application.Accounts.Commands.UpdateAccount;
using Application.Accounts.Commands.DeleteAccount;
using Application.Accounts.Queries.GetBalance;
using Application.Accounts.Queries.GetAllAccounts;
using Application.Accounts.Queries.GetAccount;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        var result = await _mediator.Send(new GetAccountByIdQuery(id));
        if (result == null)
            return NotFound();
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllAccountsQuery());
        return Ok(result);
    }

    [HttpGet("{id}/balance")]
    public async Task<IActionResult> GetBalance(int id)
    {
        var result = await _mediator.Send(new GetAccountBalanceQuery(id));
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountCommand command)
    {
        command.SetAccountId(id); 

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();//edit 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteAccountCommand { AccountId = id });
        return success ? NoContent() : NotFound();
    }

    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception("This is a test error");
    }

    [HttpGet("test-global")]
    public IActionResult TestGlobalHandler()
    {
        throw new Exception(" GlobalExceptionHandler.");
    }


}
