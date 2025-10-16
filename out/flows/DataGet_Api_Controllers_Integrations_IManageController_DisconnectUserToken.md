[web] DELETE /api/imanage/disconnect-user-token  (DataGet.Api.Controllers.Integrations.IManageController.DisconnectUserToken)  [L166–L177] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method DeleteCurrentConnectionAsync [L172]
      └─ implementation IConnectionService.DeleteCurrentConnectionAsync [L44-L45]
      └─ ... (no implementation details available)
  └─ sends_request DisconnectIManageUserTokenCommand [L171]
    └─ handled_by DataGet.Integrations.iManage.Api.Command.DisconnectIManageUserTokenCommandHandler.Handle [L11–L34]
      └─ uses_service UserTokenService
        └─ method GetTokenAsync [L24]
          └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync [L11-L81]

