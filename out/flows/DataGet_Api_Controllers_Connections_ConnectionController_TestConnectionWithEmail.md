[web] GET /api/connection/{apiType}/{email}/test-connection  (DataGet.Api.Controllers.Connections.ConnectionController.TestConnectionWithEmail)  [L121–L130] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request TestConnectionCommand [L125]
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

