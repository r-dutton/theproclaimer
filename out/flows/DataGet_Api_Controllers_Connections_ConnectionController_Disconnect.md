[web] DELETE /api/connection/{apiType}/disconnect  (DataGet.Api.Controllers.Connections.ConnectionController.Disconnect)  [L90–L98] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request DisconnectCommand [L94]
    └─ handled_by DataGet.Connections.App.Commands.DisconnectCommandHandler.Handle [L26–L99]
      └─ uses_service IConnectionContextProvider (InstancePerLifetimeScope)
        └─ method ConnectionContext [L64]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Connection>
        └─ method WriteQuery [L77]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<FileToken>
        └─ method WriteQuery [L92]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<UserToken>
        └─ method WriteQuery [L70]
          └─ ... (no implementation details available)
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetExternalApiFromApiType [L47]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L68]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

