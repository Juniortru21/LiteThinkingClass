using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;

namespace Application.UseCases.Transactions.Queries.GetTransaction;
public class GetTransactionQuery : GetTransactionQueryModel, IRequest<Result<GetTransactionQueryDto>>
{
    public class GetTransaccionQueryHandler(
        IRepository<Transaction> transaccionRepository) : UseCaseHandler, IRequestHandler<GetTransactionQuery, Result<GetTransactionQueryDto>>
    {
        [Obsolete]
        public async Task<Result<GetTransactionQueryDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            TelemetryConfiguration config = TelemetryConfiguration.CreateDefault();
            config.InstrumentationKey = "848e0a65-cc55-445f-ae50-a839661109fa";
            QuickPulseTelemetryProcessor? quickPulseProcessor = null;
            config.DefaultTelemetrySink.TelemetryProcessorChainBuilder
                .Use((next) =>
                {
                    quickPulseProcessor = new QuickPulseTelemetryProcessor(next);
                    return quickPulseProcessor;
                })
                .Build();

            var quickPulseModule = new QuickPulseTelemetryModule();

            // Secure the control channel.
            // This is optional, but recommended.
            quickPulseModule.AuthenticationApiKey = "YOUR-API-KEY-HERE";
            quickPulseModule.Initialize(config);
            quickPulseModule.RegisterTelemetryProcessor(quickPulseProcessor);

            // Create a TelemetryClient instance. It is important
            // to use the same TelemetryConfiguration here as the one
            // used to set up Live Metrics.
            TelemetryClient client = new TelemetryClient(config);

            client.TrackRequest("GetTransactionQuery", DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(230), "200", true);

            var transaction = await transaccionRepository.GetByIdAsync(request.Id) ?? throw (new ArgumentException("The transaction id does not exist"));

            var resultData = new GetTransactionQueryDto()
            {
                Id = transaction.Id,
                Value = transaction.Value,
                Status = transaction.Status,
                Date = transaction.Date
            };

            client.TrackException(new Exception("Exception Test"));

            return this.Succeded(resultData);
        }
    }
}
