using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;

namespace Application.UseCases.Transactions.Queries.GetTransaction;
public class GetTransactionQuery : GetTransactionQueryModel, IRequest<Result<GetTransactionQueryDto>>
{
    public class GetTransaccionQueryHandler(
        IRepository<Transaction> transaccionRepository) : UseCaseHandler, IRequestHandler<GetTransactionQuery, Result<GetTransactionQueryDto>>
    {
        public async Task<Result<GetTransactionQueryDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transaction = await transaccionRepository.GetByIdAsync(request.Id) ?? throw (new ArgumentException("The transaction id does not exist"));

            var resultData = new GetTransactionQueryDto()
            {
                Id = transaction.Id,
                Value = transaction.Value,
                Status = transaction.Status,
                Date = transaction.Date
            };

            return this.Succeded(resultData);
        }
    }
}
