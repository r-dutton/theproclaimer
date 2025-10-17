[web] DELETE /api/connection/{apiType}/disconnect  (DataGet.Api.Controllers.Connections.ConnectionController.Disconnect)  [L90–L98] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request DisconnectCommand -> DisconnectCommandHandler [L94]
    └─ handled_by DataGet.Connections.App.Commands.DisconnectCommandHandler.Handle [L26–L99]
      └─ calls FileTokenRepository (methods: Remove,WriteQuery) [L97]
      └─ calls UserTokenRepository (methods: Remove,WriteQuery) [L75]
      └─ uses_service IControlledRepository<Connection> (Scoped (inferred))
        └─ method WriteQuery [L77]
          └─ implementation DataGet.Data.Repository.Connections.ConnectionRepository.WriteQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L68]
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IConnectionContextProvider (InstancePerLifetimeScope)
        └─ method ConnectionContext [L64]
          └─ ... (no implementation details available)
      └─ uses_service IExternalApiServiceFactory (InstancePerLifetimeScope)
        └─ method GetExternalApiFromApiType [L47]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ DisconnectCommand
    └─ handlers 1
      └─ DisconnectCommandHandler

