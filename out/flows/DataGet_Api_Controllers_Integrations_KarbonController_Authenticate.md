[web] PUT /api/karbon/authenticate  (DataGet.Api.Controllers.Integrations.KarbonController.Authenticate)  [L34–L51] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service UserTokenService
    └─ method SaveTokenAsync [L47]
      └─ implementation DataGet.Connections.App.Services.UserTokenService.SaveTokenAsync [L11-L81]
        └─ calls UserTokenRepository (methods: Remove,WriteQuery,Add,ReadQuery) [L69]
        └─ uses_service ConnectionContextProvider
          └─ method GetTokenAsync [L28]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ UserTokenService

