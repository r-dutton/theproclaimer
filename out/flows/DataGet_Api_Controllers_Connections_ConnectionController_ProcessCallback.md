[web] GET /api/connection/process-callback  (DataGet.Api.Controllers.Connections.ConnectionController.ProcessCallback)  [L56–L85] status=200 [AllowAnonymous]
  └─ sends_request ProcessCallbackCommand -> ProcessCallbackCommandHandler [L74]
    └─ handled_by DataGet.Connections.App.Commands.ProcessCallbackCommandHandler.Handle [L24–L92]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L73]
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetExternalApiFromApiType [L69]
          └─ ... (no implementation details available)
      └─ uses_service IConnectionContextProvider (InstancePerLifetimeScope)
        └─ method AssignActiveConnectionAsync [L67]
          └─ ... (no implementation details available)
      └─ uses_service RequestContextProvider
        └─ method AssignActiveContext [L66]
          └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.AssignActiveContext
      └─ uses_service IConnectionService (InstancePerLifetimeScope)
        └─ method GetConnectionAsync [L48]
          └─ implementation Workpapers.Next.Services.ConnectionService.GetConnectionAsync [L24-L211]
            └─ uses_service ApiService (SingleInstance)
              └─ method GetAccountingFiles [L81]
                └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetAccountingFiles [L14-L75]
  └─ impact_summary
    └─ requests 1
      └─ ProcessCallbackCommand
    └─ handlers 1
      └─ ProcessCallbackCommandHandler

