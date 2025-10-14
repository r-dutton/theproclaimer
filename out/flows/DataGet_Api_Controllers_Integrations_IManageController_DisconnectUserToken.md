[web] DELETE /api/imanage/disconnect-user-token  (DataGet.Api.Controllers.Integrations.IManageController.DisconnectUserToken)  [L166–L177] [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method DeleteCurrentConnectionAsync [L172]
  └─ sends_request DisconnectIManageUserTokenCommand [L171]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Command.DisconnectIManageUserTokenCommandHandler.Handle [L11–L34]
      └─ uses_service UserTokenService
        └─ method GetTokenAsync [L24]

