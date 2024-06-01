using Moq;
using NUnit.Framework;
using Application.Common.Interfaces;
using Application.UseCases.Transactions.Commands.DeleteTransaction;
using Domain.Entities;
using static Application.UseCases.Transactions.Commands.DeleteTransaction.RemoveTransactionCommand;

namespace Application.UnitTests.UseCases.Transactions;
public class RemoveTransactionCommandTest
{
    private readonly Mock<IRepository<Transaction>> _transactionRepositoryMock;
    private readonly DeleteTransactionCommandHandler _deleteTransactionCommandHandler;
    public RemoveTransactionCommandTest()
    {
        _transactionRepositoryMock = new Mock<IRepository<Transaction>>();
        _deleteTransactionCommandHandler = new DeleteTransactionCommandHandler(_transactionRepositoryMock.Object);
    }

    [Test]
    public void DeleteTransactionCommandShouldReturnAnExceptionIfTransactionIdDoesNotExist()
    {
        // Arrange
        _transactionRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as Transaction);
        var errorMessage = "The transaction id does not exist";

        // Action
        // Assert
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _deleteTransactionCommandHandler.Handle(new RemoveTransactionCommand
            {
                Id = Guid.NewGuid(),
            }, new CancellationToken());
        }, errorMessage);

    }
}
