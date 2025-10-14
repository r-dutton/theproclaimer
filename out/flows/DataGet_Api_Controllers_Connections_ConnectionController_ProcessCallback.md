[web] GET /api/connection/process-callback  (DataGet.Api.Controllers.Connections.ConnectionController.ProcessCallback)  [L56–L85] [AllowAnonymous]
  └─ sends_request ProcessCallbackCommand [L74]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Commands.ProcessCallbackCommandHandler.Handle [L24–L92]
      └─ uses_service IConnectionContextProvider (InstancePerLifetimeScope)
        └─ method AssignActiveConnectionAsync [L67]
      └─ uses_service IConnectionService (InstancePerLifetimeScope)
        └─ method GetConnectionAsync [L48]
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetExternalApiFromApiType [L69]
      └─ uses_service RequestContextProvider
        └─ method AssignActiveContext [L66]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L73]

