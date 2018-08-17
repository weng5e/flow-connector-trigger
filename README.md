# Invoke Microsoft Flow (Microsoft Logic app) using custom connector with WebHook trigger
[Microsoft Flow](https://flow.microsoft.com/) can help automating repetitive tasks.
The Flow service already did a really well integration with a variety of applications ans services,
e.g. send Outlook email, send tweet, read Dropbox document and so on. Microsoft Flow can act
as a broker between your application and those applications. By doing so, it can elevate
the burden of building the integrations to different applications.

If you want to invoke Flow from your application, one simple way is to add the 'Request connector'
to a Flow. Then that connecter will expose a HTTP endpoint that you can use as RSET API call from
your application. [This blog](https://flow.microsoft.com/en-us/blog/call-flow-restapi/) provided a sample
about this. However, this requires the flow creator to manaully input the request schema in the 'Request connector'
and register the HTTP endpoint to your application. ['Custom connector'](https://docs.microsoft.com/en-us/connectors/custom-connectors/index)
can improve this procedure.

You can also invoke Flow from your application through a ['Custom connector trigger'](https://docs.microsoft.com/en-us/connectors/custom-connectors/index) you defined.
There are two tyoes of trigger: Polling and [Webhook](https://go.microsoft.com/fwlink/?LinkID=851044). This sample only focus on the Webhook trigger.

The sample web application in this Repo is for demostrating the integration of your
application with Microsoft Flow. 

## File structure
### Sample web app
The solution ['src/FlowConnectorTrigger.sln'](https://github.com/weng5e/flow-connector-trigger/blob/master/src/FlowConnectorTrigger.sln) 
is the sample web app that will integrate with Flow to invoke Flow's functionality. It is an Asp.Net Core 2.1 Web API application.
You can run the application in Visual Studio 2017 (requiring [.Net Core 2.1 SDK](https://www.microsoft.com/net/download/dotnet-core/2.1)).

### Sample custom connector OpenAPI definitions
The folder ['connector-definition'](https://github.com/weng5e/flow-connector-trigger/tree/master/connector-definition) contains the OpenAPI (Swagger) files
of several custom connectors.
