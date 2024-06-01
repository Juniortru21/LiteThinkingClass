using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Application.Common.Interfaces;
using Application.Common.Utils;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;
namespace Application.UseCases.Transactions.Queries.GetTransactions;

public class GetTransactionsQuery : IRequest<Result<GetTransactionsQueryDto>>
{
    public class GetTransactionsQueryHandler(
        IConfiguration configuration,
        IRepository<Transaction> transaccionRepository) : UseCaseHandler, IRequestHandler<GetTransactionsQuery, Result<GetTransactionsQueryDto>>
    {
        public async Task<Result<GetTransactionsQueryDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var path = configuration.GetSection("FilePath").Value;
            //var fileName = configuration.GetSection("FileName").Value;
            var transactions = await transaccionRepository.GetAllAsync();

            //await Document<Transaction>.SaveToCsv(transactions, $"{path}{fileName}");

            //var fileStream = Document<Transaction>.ImportCsv($"{path}{fileName}");

            //var blobStorage = new BlobStorageService(configuration);
            //await blobStorage.DownloadFile();
            //await blobStorage.UploadFile(fileStream);


            var transactionsDto = transactions
                    .Select(x => new GetTransactionsQueryValueDto()
                    {
                        Id = x.Id,
                        Value = x.Value,
                        Status = x.Status,
                        Date = x.Date
                    });

            var resultData = new GetTransactionsQueryDto()
            {
                Transactions = transactionsDto
            };

            return this.Succeded(resultData);
        }
    }
}
