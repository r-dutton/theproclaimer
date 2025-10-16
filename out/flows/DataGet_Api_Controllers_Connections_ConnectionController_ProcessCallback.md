[web] GET /api/connection/process-callback  (DataGet.Api.Controllers.Connections.ConnectionController.ProcessCallback)  [L56–L85] status=200 [AllowAnonymous]
  └─ sends_request ProcessCallbackCommand [L74]
    └─ handled_by DataGet.Connections.App.Commands.ProcessCallbackCommandHandler.Handle [L24–L92]
      └─ uses_service IConnectionContextProvider (InstancePerLifetimeScope)
        └─ method AssignActiveConnectionAsync [L67]
          └─ ... (no implementation details available)
      └─ uses_service IConnectionService (InstancePerLifetimeScope)
        └─ method GetConnectionAsync [L48]
          └─ implementation IConnectionService.GetConnectionAsync [L44-L45]
          └─ ... (no implementation details available)
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetExternalApiFromApiType [L69]
          └─ ... (no implementation details available)
      └─ uses_service RequestContextProvider
        └─ method AssignActiveContext [L66]
          └─ implementation DataGet.ApplicationService.Services.RequestContextProvider.AssignActiveContext [L11-L76]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L73]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

