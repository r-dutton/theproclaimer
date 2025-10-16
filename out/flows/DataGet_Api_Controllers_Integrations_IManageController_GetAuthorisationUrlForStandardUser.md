[web] GET /api/imanage/standard-user-authorisation-url  (DataGet.Api.Controllers.Integrations.IManageController.GetAuthorisationUrlForStandardUser)  [L67–L92] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method GetCurrentUsersConnection [L70]
      └─ implementation IConnectionService.GetCurrentUsersConnection [L44-L45]
      └─ ... (no implementation details available)
  └─ uses_service IIntegrationConfigService (InstancePerLifetimeScope)
    └─ method GetApiIntegrationConfig [L73]
      └─ implementation DataGet.Connections.App.Services.IntegrationConfigService.GetApiIntegrationConfig [L8-L37]
  └─ sends_request CreateOrUpdateIntegrationConfigCommand [L85]
    └─ handled_by DataGet.Connections.App.Commands.CreateOrUpdateIntegrationConfigCommandHandler.Handle [L25–L52]
      └─ uses_service IControlledRepository<IntegrationConfiguration>
        └─ method WriteQuery [L36]
          └─ ... (no implementation details available)
  └─ sends_request GetIManageAuthorizationUrlQuery [L82]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetAuthorizationUrlQueryHandler.Handle [L25–L41]
      └─ uses_service IManageService
        └─ method GetAuthorisationUrl [L38]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetAuthorisationUrl [L12-L223]

