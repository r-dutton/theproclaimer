[web] GET /api/cloud-capcha/test-connection  (DataGet.Api.Controllers.Integrations.CloudCapchaController.TestConnection)  [L93–L108] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IApiTokenService (InstancePerLifetimeScope)
    └─ method GetTokenAsync [L98]
      └─ implementation DataGet.Connections.App.Services.ApiTokenService.GetTokenAsync [L11-L61]

