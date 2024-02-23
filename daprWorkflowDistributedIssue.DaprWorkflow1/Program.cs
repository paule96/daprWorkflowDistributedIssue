// See https://aka.ms/new-console-template for more information
using Dapr.Client;
using Dapr.Workflow;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
{
    services.AddDaprWorkflowClient();
    services.AddDaprWorkflow(options =>
    {
        options.RegisterActivity<CallWeather>();
        options.RegisterWorkflow<string, IEnumerable<int>>("CallWeatherWorkflow", async (c, i) =>
        {
            return await c.CallActivityAsync<IEnumerable<int>>(nameof(CallWeather));
        });
    });
});

// Start the app - this is the point where we connect to the Dapr sidecar
using var host = builder.Build();
host.Run();

 record WeatherResult(int TemperatureC);
 internal class CallWeather : Dapr.Workflow.WorkflowActivity<string, IEnumerable<int>>
 {
     private readonly DaprClient client;

     public CallWeather(DaprClient client)
     {
         this.client = client;
     }
     public override async Task<IEnumerable<int>> RunAsync(WorkflowActivityContext context, string input)
     {
         var weather = await client.InvokeMethodAsync<IEnumerable<WeatherResult>>(HttpMethod.Get, "WeatherApi", "WeatherForecast");
         return weather.Select(w => w.TemperatureC);
     }
 }