[web] DELETE /api/imanage/disconnect  (DataGet.Api.Controllers.Integrations.IManageController.Disconnect)  [L148–L164] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method GetConnectionsAsync [L154]
      └─ implementation Workpapers.Next.Services.ConnectionService.GetConnectionsAsync [L24-L211]
        └─ uses_service ApiService (SingleInstance)
          └─ method GetAccountingFiles [L81]
            └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetAccountingFiles [L14-L75]
  └─ sends_request DeleteIntegrationConfigCommand -> DeleteIntegrationConfigCommandHandler [L159]
    └─ handled_by DataGet.Connections.App.Commands.DeleteIntegrationConfigCommandHandler.Handle [L19–L41]
      └─ uses_service IControlledRepository<IntegrationConfiguration> (Scoped (inferred))
        └─ method WriteQuery [L30]
          └─ implementation DataGet.Data.Repository.Connections.IntegrationConfigRepository.WriteQuery [L5-L35]
  └─ sends_request DisconnectIManageUserTokensCommand -> DisconnectIManageUserTokensCommandHandler [L157]
    └─ handled_by DataGet.Integrations.iManage.Api.Command.DisconnectIManageUserTokensCommandHandler.Handle [L17–L40]
      └─ uses_service UserTokenService
        └─ method GetAllUserTokensAsync [L30]
          └─ implementation DataGet.Connections.App.Services.UserTokenService.GetAllUserTokensAsync [L11-L81]
            └─ calls UserTokenRepository (methods: Remove,WriteQuery,Add,ReadQuery) [L69]
            └─ uses_service ConnectionContextProvider
              └─ method GetTokenAsync [L28]
                └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ IConnectionService
    └─ requests 2
      └─ DeleteIntegrationConfigCommand
      └─ DisconnectIManageUserTokensCommand
    └─ handlers 2
      └─ DeleteIntegrationConfigCommandHandler
      └─ DisconnectIManageUserTokensCommandHandler

