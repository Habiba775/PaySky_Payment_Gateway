using Application.Transactions.Commands.Deposit;

using Application.Transactions.Commands.Withdraw;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Services;
using Application.Transactions.Commands.Tansfer.Application.Transactions.Commands;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _transactionRepository;
        private readonly PdfReceiptGenerator _pdfReceiptGenerator;

        public TransactionsController(
            IMediator mediator,
            ITransactionRepository transactionRepository,
            PdfReceiptGenerator pdfReceiptGenerator)
        {
            _mediator = mediator;
            _transactionRepository = transactionRepository;
            _pdfReceiptGenerator = pdfReceiptGenerator;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var transactionId = await _mediator.Send(command);
                return Ok(new { TransactionId = transactionId });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var transactionId = await _mediator.Send(command);
                return Ok(new { TransactionId = transactionId });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var transactionId = await _mediator.Send(command);
                return Ok(new { TransactionId = transactionId });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("{id}/receipt")]
        public async Task<IActionResult> GenerateReceipt(int id)
        {
            try
            {
                
                var transaction = await _transactionRepository.GetByIdAsync(id);
                if (transaction == null)
                    return NotFound();

                
                var pdfBytes = _pdfReceiptGenerator.Generate(transaction);

                
                return File(pdfBytes, "application/pdf", $"Receipt_Transaction_{id}.pdf");
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
