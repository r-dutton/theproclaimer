[web] DELETE /api/imanage/disconnect  (DataGet.Api.Controllers.Integrations.IManageController.Disconnect)  [L148–L164] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method GetConnectionsAsync [L154]
      └─ implementation IConnectionService.GetConnectionsAsync [L44-L45]
      └─ ... (no implementation details available)
  └─ sends_request DeleteIntegrationConfigCommand [L159]
    └─ handled_by DataGet.Connections.App.Commands.DeleteIntegrationConfigCommandHandler.Handle [L19–L41]
      └─ uses_service IControlledRepository<IntegrationConfiguration>
        └─ method WriteQuery [L30]
          └─ ... (no implementation details available)
  └─ sends_request DisconnectIManageUserTokensCommand [L157]
    └─ handled_by DataGet.Integrations.iManage.Api.Command.DisconnectIManageUserTokensCommandHandler.Handle [L17–L40]
      └─ uses_service UserTokenService
        └─ method GetAllUserTokensAsync [L30]
          └─ implementation DataGet.Connections.App.Services.UserTokenService.GetAllUserTokensAsync [L11-L81]

