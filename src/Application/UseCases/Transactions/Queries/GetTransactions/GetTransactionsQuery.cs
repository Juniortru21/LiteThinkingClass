using Application.Common.Interfaces;
using Application.Common.Utils;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Common.Utils;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
namespace Application.UseCases.Transactions.Queries.GetTransactions;

public class GetTransactionsQuery : IRequest<Result<GetTransactionsQueryDto>>
{
    public class GetTransactionsQueryHandler(
        IConfiguration configuration,
        IRepository<Transaction> transaccionRepository) : UseCaseHandler, IRequestHandler<GetTransactionsQuery, Result<GetTransactionsQueryDto>>
    {
        public async Task<Result<GetTransactionsQueryDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var logs = new Logs(configuration);
            await logs.AddLog($"Start the GetTransactionsQuery logs", "Admin");
            await logs.SearchLog();
            var path = configuration.GetSection("FilePath").Value;
            var fileName = configuration.GetSection("FileName").Value ?? string.Empty;
            var extension = configuration.GetSection("Extension").Value ?? string.Empty;

            var transactions = await transaccionRepository.GetAllAsync();

            transactions.ToList().ForEach(async x =>
            {
                await Document<Transaction>.SaveToCsv(transactions.Where(y => y == x), $"{path}{fileName}{x.File}.{extension}");

                var fileStream = Document<Transaction>.ImportCsv($"{path}{fileName}{x.File}.{extension}");

                var blobStorage = new BlobStorageService(configuration);

                await blobStorage.UploadFile($"{fileName}{x.File}", extension, fileStream);
                await blobStorage.DownloadFile();
            });

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
