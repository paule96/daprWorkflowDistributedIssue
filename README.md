# daprWorkflowDistributedIssue
Tries to provide an example on distributed workflow that is currently not working.

## Run sample

1. `dapr init`
1. `dotnet workload install aspire`
1. Run the project `daprWorkflowDistributedIssue.AppHost.csproj` 
1. Go to the aspire dashboard
1. search for service `daprWorkflowDistributedIssue_DaprWorkflow1-dapr-cli` and `daprWorkflowDistributedIssue_DaprWorkflow2-dapr-cli`
1. Open the log view of both and note the port
1. open the file `requests.http` and replace the values for the ports
1. run the request. The first two requests will work. (it just calls a workflow directly)
1. run the last two requests, and you will see that the workflow one can't find the workflow two.