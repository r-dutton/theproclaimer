[web] POST /api/imanage/authorisation-url  (DataGet.Api.Controllers.Integrations.IManageController.GetAuthorisationUrl)  [L50–L65] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method GetCurrentUsersConnection [L53]
      └─ implementation IConnectionService.GetCurrentUsersConnection [L44-L45]
      └─ ... (no implementation details available)
  └─ sends_request CreateOrUpdateIntegrationConfigCommand [L58]
    └─ handled_by DataGet.Connections.App.Commands.CreateOrUpdateIntegrationConfigCommandHandler.Handle [L25–L52]
      └─ uses_service IControlledRepository<IntegrationConfiguration>
        └─ method WriteQuery [L36]
          └─ ... (no implementation details available)
  └─ sends_request GetIManageAuthorizationUrlQuery [L55]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetAuthorizationUrlQueryHandler.Handle [L25–L41]
      └─ uses_service IManageService
        └─ method GetAuthorisationUrl [L38]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetAuthorisationUrl [L12-L223]

