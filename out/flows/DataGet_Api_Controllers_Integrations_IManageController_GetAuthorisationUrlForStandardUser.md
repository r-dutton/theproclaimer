[web] GET /api/imanage/standard-user-authorisation-url  (DataGet.Api.Controllers.Integrations.IManageController.GetAuthorisationUrlForStandardUser)  [L67–L92] [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method GetCurrentUsersConnection [L70]
  └─ uses_service IIntegrationConfigService (InstancePerLifetimeScope)
    └─ method GetApiIntegrationConfig [L73]
  └─ sends_request CreateOrUpdateIntegrationConfigCommand [L85]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Commands.CreateOrUpdateIntegrationConfigCommandHandler.Handle [L25–L52]
      └─ uses_service IControlledRepository<IntegrationConfiguration>
        └─ method WriteQuery [L36]
  └─ sends_request GetIManageAuthorizationUrlQuery [L82]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetAuthorizationUrlQueryHandler.Handle [L25–L41]
      └─ uses_service IManageService
        └─ method GetAuthorisationUrl [L38]

