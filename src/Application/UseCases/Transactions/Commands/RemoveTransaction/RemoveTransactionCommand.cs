using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;

namespace Application.UseCases.Transactions.Commands.DeleteTransaction;
public class RemoveTransactionCommand : RemoveTransactionCommandModel, IRequest<Result<RemoveTransactionCommandDto>>
{
    public class DeleteTransactionCommandHandler(
        IRepository<Transaction> transactionRepository) : UseCaseHandler, IRequestHandler<RemoveTransactionCommand, Result<RemoveTransactionCommandDto>>
    {
        public async Task<Result<RemoveTransactionCommandDto>> Handle(RemoveTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction =
                    await transactionRepository.GetByIdAsync(request.Id)
                    ?? throw (new ArgumentException("The transaction id does not exist"));

            await transactionRepository.RemoveAsync(transaction);

            var resultData = new RemoveTransactionCommandDto { Success = true };

            return this.Succeded(resultData);
        }
    }
}
