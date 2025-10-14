[web] DELETE /api/imanage/disconnect  (DataGet.Api.Controllers.Integrations.IManageController.Disconnect)  [L148–L164] [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method GetConnectionsAsync [L154]
  └─ sends_request DeleteIntegrationConfigCommand [L159]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Commands.DeleteIntegrationConfigCommandHandler.Handle [L19–L41]
      └─ uses_service IControlledRepository<IntegrationConfiguration>
        └─ method WriteQuery [L30]
  └─ sends_request DisconnectIManageUserTokensCommand [L157]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Command.DisconnectIManageUserTokensCommandHandler.Handle [L17–L40]
      └─ uses_service UserTokenService
        └─ method GetAllUserTokensAsync [L30]

