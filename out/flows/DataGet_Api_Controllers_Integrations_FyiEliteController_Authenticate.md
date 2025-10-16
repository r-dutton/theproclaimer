[web] PUT /api/fyi-elite/authenticate  (DataGet.Api.Controllers.Integrations.FyiEliteController.Authenticate)  [L36–L62] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service UserTokenService
    └─ method GetTokenAsync [L41]
      └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync [L11-L81]
        └─ uses_service ConnectionContextProvider
          └─ method GetTokenAsync [L28]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ UserTokenService

