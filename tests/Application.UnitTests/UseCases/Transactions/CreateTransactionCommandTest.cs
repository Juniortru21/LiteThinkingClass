using Moq;
using NUnit.Framework;
using Application.Common.Interfaces;
using Application.UseCases.Common.Results;
using Application.UseCases.Transactions.Commands.CreateTransaction;
using Domain.Entities;
using static Application.UseCases.Transactions.Commands.CreateTransaction.CreateTransactionCommand;

namespace Application.UnitTests.UseCases.Transactions;
public class CreateTransactionCommandTest
{
    private readonly Mock<IRepository<Transaction>> _transactionRepositoryMock;
    private readonly CreateTransactionCommandHandler _createTransactionCommandHandler;
    public CreateTransactionCommandTest()
    {
        _transactionRepositoryMock = new Mock<IRepository<Transaction>>();
        _createTransactionCommandHandler = new CreateTransactionCommandHandler(_transactionRepositoryMock.Object);
    }

    [Test]
    public async Task CreateTransactionCommandShouldReturnSuccessAsResult()
    {
        // Arrange
        _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>()));

        // Action
        var result = await _createTransactionCommandHandler.Handle(new CreateTransactionCommand
        {
            Value = 123,
            Status = true
        }, new CancellationToken());

        // Assert
        Assert.That(result.ResultType, Is.EqualTo(ResultType.Ok));
        
    }
}
