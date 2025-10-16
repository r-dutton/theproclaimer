[web] GET /api/connection/{apiType}/{email}/test-connection  (DataGet.Api.Controllers.Connections.ConnectionController.TestConnectionWithEmail)  [L121–L130] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request TestConnectionCommand -> TestConnectionCommandHandler [L125]
    └─ handled_by DataGet.Connections.App.Commands.TestConnectionCommandHandler.Handle [L25–L61]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L47]
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetExternalApiFromApiType [L38]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ TestConnectionCommand
    └─ handlers 1
      └─ TestConnectionCommandHandler

