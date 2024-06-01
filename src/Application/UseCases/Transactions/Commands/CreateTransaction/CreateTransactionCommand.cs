using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;

namespace Application.UseCases.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommand : CreateTransactionCommandModel, IRequest<Result<CreateTransactionCommandDto>>
{
    public class CreateTransactionCommandHandler(
        IRepository<Transaction> transactionRepository) : UseCaseHandler, IRequestHandler<CreateTransactionCommand, Result<CreateTransactionCommandDto>>
    {
        public async Task<Result<CreateTransactionCommandDto>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction
            {
                Value = request.Value,
                Status = request.Status,
                Date = DateTime.UtcNow,
            };

            await transactionRepository.AddAsync(transaction);

            var resultData = new CreateTransactionCommandDto { Success = true };

            return Succeded(resultData);
        }
    }
}
