[web] GET /api/karbon/test-connection  (DataGet.Api.Controllers.Integrations.KarbonController.TestConnection)  [L53–L62] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request TestConnectionQuery [L58]
    └─ handled_by Cirrus.Connections.DataGet.Queries.TestConnectionQueryHandler.Handle [L17–L31]
      └─ uses_client DataGetClient [L28]
        └─ calls TestConnection (GET /api/connection/{apiType}/test-connection?fileId={*}&password={*}&username={*}, method=TestConnectionAsync, target=DataGet.Api, query=fileId={*}&password={*}&username={*}) [L472]
          └─ target_service DataGet.Api
            └─ [web] GET /api/connection/{apiType}/test-connection  (DataGet.Api.Controllers.Connections.ConnectionController.TestConnection)  [L110–L119] status=200 [auth=Authentication.MachineToMachinePolicy]
              └─ sends_request TestConnectionCommand [L114]
                └─ handled_by DataGet.Connections.App.Commands.TestConnectionCommandHandler.Handle [L25–L61]
                  └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
                    └─ method GetExternalApiFromApiType [L38]
                      └─ ... (no implementation details available)
                  └─ uses_service RequestProcessor
                    └─ method ProcessAsync [L47]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
                        └─ constructs RequestProcessorWrapper<TRequest,TResult>
                        └─ resolves IPipelineBehavior<TRequest,TResult> chain
                        └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service DataGetClient
        └─ method TestConnectionAsync [L28]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.TestConnectionAsync [L15-L302]
            └─ calls TestConnection [L84]

