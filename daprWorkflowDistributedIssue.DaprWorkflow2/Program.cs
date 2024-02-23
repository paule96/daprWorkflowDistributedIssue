// See https://aka.ms/new-console-template for more information
using Dapr.Workflow;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddDaprWorkflow(options =>
    {
        options.RegisterWorkflow<GetWeatherAndMood>();
    });
});

// Start the app - this is the point where we connect to the Dapr sidecar
using var host = builder.Build();
host.Run();

record WorkflowResult(int temp, string summary);
class GetWeatherAndMood : Workflow<string, IEnumerable<WorkflowResult>>
{
    public override async Task<IEnumerable<WorkflowResult>> RunAsync(WorkflowContext context, string input)
    {
        var weatherTemps = await context.CallChildWorkflowAsync<IEnumerable<int>>("CallWeatherWorkflow", string.Empty);
        return Enumerable.Empty<WorkflowResult>();
    }
}