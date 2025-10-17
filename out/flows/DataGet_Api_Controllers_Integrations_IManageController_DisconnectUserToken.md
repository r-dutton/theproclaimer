[web] DELETE /api/imanage/disconnect-user-token  (DataGet.Api.Controllers.Integrations.IManageController.DisconnectUserToken)  [L166–L177] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method DeleteCurrentConnectionAsync [L172]
      └─ implementation Workpapers.Next.Services.ConnectionService.DeleteCurrentConnectionAsync [L24-L211]
        └─ uses_service ApiService (SingleInstance)
          └─ method GetAccountingFiles [L81]
            └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetAccountingFiles [L14-L75]
  └─ sends_request DisconnectIManageUserTokenCommand -> DisconnectIManageUserTokenCommandHandler [L171]
    └─ handled_by DataGet.Integrations.iManage.Api.Command.DisconnectIManageUserTokenCommandHandler.Handle [L11–L34]
      └─ uses_service UserTokenService
        └─ method GetTokenAsync [L24]
          └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync [L11-L81]
            └─ uses_service ConnectionContextProvider
              └─ method GetTokenAsync [L28]
                └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ IConnectionService
    └─ requests 1
      └─ DisconnectIManageUserTokenCommand
    └─ handlers 1
      └─ DisconnectIManageUserTokenCommandHandler

