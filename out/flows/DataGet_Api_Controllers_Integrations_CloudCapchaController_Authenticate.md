[web] PUT /api/cloud-capcha/authenticate  (DataGet.Api.Controllers.Integrations.CloudCapchaController.Authenticate)  [L45–L75] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IApiTokenService (InstancePerLifetimeScope)
    └─ method GetTokenAsync [L50]
      └─ implementation DataGet.Connections.App.Services.ApiTokenService.GetTokenAsync [L11-L61]

