using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;

namespace Api;

public static class DependencyInjection
{
    [Obsolete]
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Create a TelemetryConfiguration instance.
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

        // This sample runs indefinitely. Replace with actual application logic.
        while (true)
        {
            // Send dependency and request telemetry.
            // These will be shown in Live Metrics.
            // CPU/Memory Performance counter is also shown
            // automatically without any additional steps.
            client.TrackDependency("My dependency", "target", "http://sample",
                DateTimeOffset.Now, TimeSpan.FromMilliseconds(300), true);
            client.TrackRequest("My Request", DateTimeOffset.Now,
                TimeSpan.FromMilliseconds(230), "200", true);

            client.TrackException(new Exception("Exception Telemerty"));
            Task.Delay(500).Wait();
        }
    }
}
