[web] GET /api/cloud-capcha/access-info  (DataGet.Api.Controllers.Integrations.CloudCapchaController.GetAccessInfo)  [L77–L91] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IApiTokenService (InstancePerLifetimeScope)
    └─ method GetTokenAsync [L82]
      └─ implementation DataGet.Connections.App.Services.ApiTokenService.GetTokenAsync [L11-L61]

