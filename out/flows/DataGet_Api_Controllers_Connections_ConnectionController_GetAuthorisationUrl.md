[web] GET /api/connection/{apiType}/authorisation-url  (DataGet.Api.Controllers.Connections.ConnectionController.GetAuthorisationUrl)  [L46–L54] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetAuthorizationUrlQuery -> GetAuthorizationUrlQueryHandler [L50]
    └─ handled_by DataGet.Connections.App.Queries.GetAuthorizationUrlQueryHandler.Handle [L23–L60]
      └─ uses_service RequestProcessor
        └─ method Process [L53]
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.Process
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.Process
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.Process
          └─ +1 additional_requests elided
      └─ uses_service ExternalApiServiceFactory
        └─ method GetExternalApiFromApiType [L51]
          └─ ... (no implementation details available)
      └─ uses_service ConnectionContextProvider
        └─ method ConnectionContext [L39]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ GetAuthorizationUrlQuery
    └─ handlers 1
      └─ GetAuthorizationUrlQueryHandler

