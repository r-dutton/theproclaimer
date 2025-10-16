[web] GET /api/fyi/verify-connection  (DataGet.Api.Controllers.Integrations.FyiController.VerifyConnection)  [L304–L317] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service UserTokenService
    └─ method GetTokenAsync [L309]
      └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync [L11-L81]
      └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync [L11-L81]

