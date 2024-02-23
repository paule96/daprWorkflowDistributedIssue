using Projects;
using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<daprWorkflowDistributedIssue_ApiService>("WeatherApi")
    .WithDaprSidecar();

builder.AddProject<daprWorkflowDistributedIssue_DaprWorkflow1>(nameof(daprWorkflowDistributedIssue_DaprWorkflow1))
    .WithDaprSidecar();
builder.AddProject<daprWorkflowDistributedIssue_DaprWorkflow2>(nameof(daprWorkflowDistributedIssue_DaprWorkflow2))
    .WithDaprSidecar();

builder.Build().Run();
