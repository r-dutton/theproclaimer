[web] GET /api/connection/{apiType}/authorisation-url  (DataGet.Api.Controllers.Connections.ConnectionController.GetAuthorisationUrl)  [L46–L54] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetAuthorizationUrlQuery [L50]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Queries.GetAuthorizationUrlQueryHandler.Handle [L23–L60]
      └─ uses_service ConnectionContextProvider
        └─ method ConnectionContext [L39]
      └─ uses_service ExternalApiServiceFactory
        └─ method GetExternalApiFromApiType [L51]
      └─ uses_service RequestProcessor
        └─ method Process [L53]

