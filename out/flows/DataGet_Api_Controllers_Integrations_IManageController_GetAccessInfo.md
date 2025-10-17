[web] GET /api/imanage/access-info  (DataGet.Api.Controllers.Integrations.IManageController.GetAccessInfo)  [L115–L136] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IIntegrationConfigService (InstancePerLifetimeScope)
    └─ method GetDisplayIntegrationConfigInfo [L123]
      └─ implementation DataGet.Connections.App.Services.IntegrationConfigService.GetDisplayIntegrationConfigInfo [L8-L37]
        └─ uses_service IControlledRepository<IntegrationConfiguration> (Scoped (inferred))
          └─ method GetApiIntegrationConfig [L19]
            └─ implementation DataGet.Data.Repository.Connections.IntegrationConfigRepository.GetApiIntegrationConfig [L5-L35]
  └─ impact_summary
    └─ services 1
      └─ IIntegrationConfigService

