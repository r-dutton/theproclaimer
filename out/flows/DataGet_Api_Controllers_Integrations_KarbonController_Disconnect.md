[web] DELETE /api/karbon/disconnect  (DataGet.Api.Controllers.Integrations.KarbonController.Disconnect)  [L64–L75] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service UserTokenService
    └─ method GetTokenAsync [L69]
      └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync [L11-L81]
        └─ uses_service ConnectionContextProvider
          └─ method GetTokenAsync [L28]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ UserTokenService

