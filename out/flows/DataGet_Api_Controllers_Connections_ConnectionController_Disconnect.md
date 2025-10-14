[web] DELETE /api/connection/{apiType}/disconnect  (DataGet.Api.Controllers.Connections.ConnectionController.Disconnect)  [L90–L98] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request DisconnectCommand [L94]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Commands.DisconnectCommandHandler.Handle [L26–L99]
      └─ uses_service IConnectionContextProvider (InstancePerLifetimeScope)
        └─ method ConnectionContext [L64]
      └─ uses_service IControlledRepository<Connection>
        └─ method WriteQuery [L77]
      └─ uses_service IControlledRepository<FileToken>
        └─ method WriteQuery [L92]
      └─ uses_service IControlledRepository<UserToken>
        └─ method WriteQuery [L70]
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetExternalApiFromApiType [L47]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L68]

