[web] GET /api/imanage/access-info  (DataGet.Api.Controllers.Integrations.IManageController.GetAccessInfo)  [L115–L136] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IIntegrationConfigService (InstancePerLifetimeScope)
    └─ method GetDisplayIntegrationConfigInfo [L123]
      └─ implementation DataGet.Connections.App.Services.IntegrationConfigService.GetDisplayIntegrationConfigInfo [L8-L37]

