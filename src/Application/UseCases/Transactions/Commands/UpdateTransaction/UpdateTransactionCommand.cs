using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;

namespace Application.UseCases.Transactions.Commands.UpdateTransaction;
public class UpdateTransactionCommand : UpdateTransactionCommandModel, IRequest<Result<UpdateTransactionCommandDto>>
{
    public class UpdateTransactionCommandHandler(
        IRepository<Transaction> transactionRepository) : UseCaseHandler, IRequestHandler<UpdateTransactionCommand, Result<UpdateTransactionCommandDto>>
    {
        public async Task<Result<UpdateTransactionCommandDto>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction =
                    await transactionRepository.GetByIdAsync(request.Id)
                    ?? throw (new ArgumentException("The transaction id does not exist"));

            transaction.Value = request.Value;
            transaction.Status = request.Status;
            transaction.Date = DateTime.UtcNow;

            await transactionRepository.UpdateAsync(transaction);

            var resultData = new UpdateTransactionCommandDto { Success = true };

            return this.Succeded(resultData);
        }
    }
}
