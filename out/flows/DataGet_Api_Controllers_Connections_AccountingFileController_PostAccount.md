[web] POST /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.PostAccount)  [L89–L97] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request PostAccountCommand -> PostAccountCommandHandler [L93]
    └─ handled_by DataGet.Connections.App.Commands.PostAccountCommandHandler.Handle [L22–L43]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L41]
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetAccountingApiFromApiType [L32]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ PostAccountCommand
    └─ handlers 1
      └─ PostAccountCommandHandler

