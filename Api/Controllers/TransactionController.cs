using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Transactions.Commands.CreateTransaction;
using Application.UseCases.Transactions.Commands.DeleteTransaction;
using Application.UseCases.Transactions.Commands.UpdateTransaction;
using Application.UseCases.Transactions.Queries.GetTransaction;
using Application.UseCases.Transactions.Queries.GetTransactions;
namespace Api.Controllers;

public class TransactionController : BaseController
{
    [HttpGet]
    [Route("GetAll")]
    [Produces(typeof(GetTransactionsQueryDto))]
    [ActionName(nameof(GetAll))]
    public async Task<IActionResult> GetAll()
    {
        //
        var query = new GetTransactionsQuery();
        var result = await this.Mediator.Send(query);
        return this.FromResult(result);
    }

    [HttpGet]
    [Route("GetById")]
    [Produces(typeof(GetTransactionQueryDto))]
    [ActionName(nameof(GetById))]
    public async Task<IActionResult> GetById([FromQuery] GetTransactionQueryModel model)
    {
        var query = this.Mapper.Map<GetTransactionQuery>(model);
        var result = await this.Mediator.Send(query);
        return this.FromResult(result);
    }

    [HttpPost]
    [Route("Create")]
    [Produces(typeof(CreateTransactionCommandDto))]
    [ActionName(nameof(CreateTransaction))]
    public async Task<IActionResult> CreateTransaction(CreateTransactionCommandModel model)
    {
        var command = this.Mapper.Map<CreateTransactionCommand>(model);
        var result = await this.Mediator.Send(command);
        return this.FromResult(result);
    }

    [HttpPut]
    [Route("Update")]
    [Produces(typeof(UpdateTransactionCommandDto))]
    [ActionName(nameof(UpdateTransaction))]
    public async Task<IActionResult> UpdateTransaction(UpdateTransactionCommandModel model)
    {
        var command = this.Mapper.Map<UpdateTransactionCommand>(model);
        var result = await this.Mediator.Send(command);
        return this.FromResult(result);
    }

    [HttpDelete]
    [Route("Delete")]
    [Produces(typeof(RemoveTransactionCommandDto))]
    [ActionName(nameof(RemoveTransaction))]
    public async Task<IActionResult> RemoveTransaction(RemoveTransactionCommandModel model)
    {
        var command = this.Mapper.Map<RemoveTransactionCommand>(model);
        var result = await this.Mediator.Send(command);
        return this.FromResult(result);
    }
}
