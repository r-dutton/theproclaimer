[web] POST /api/imanage/authorisation-url  (DataGet.Api.Controllers.Integrations.IManageController.GetAuthorisationUrl)  [L50–L65] [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method GetCurrentUsersConnection [L53]
  └─ sends_request CreateOrUpdateIntegrationConfigCommand [L58]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Commands.CreateOrUpdateIntegrationConfigCommandHandler.Handle [L25–L52]
      └─ uses_service IControlledRepository<IntegrationConfiguration>
        └─ method WriteQuery [L36]
  └─ sends_request GetIManageAuthorizationUrlQuery [L55]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetAuthorizationUrlQueryHandler.Handle [L25–L41]
      └─ uses_service IManageService
        └─ method GetAuthorisationUrl [L38]

