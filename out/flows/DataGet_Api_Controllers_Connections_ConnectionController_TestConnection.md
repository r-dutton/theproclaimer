[web] GET /api/connection/{apiType}/test-connection  (DataGet.Api.Controllers.Connections.ConnectionController.TestConnection)  [L110–L119] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request TestConnectionCommand [L114]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Commands.TestConnectionCommandHandler.Handle [L25–L61]
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetExternalApiFromApiType [L38]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L47]

