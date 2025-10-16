[web] GET /api/cloud-capcha/test-connection  (DataGet.Api.Controllers.Integrations.CloudCapchaController.TestConnection)  [L93–L108] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IApiTokenService (InstancePerLifetimeScope)
    └─ method GetTokenAsync [L98]
      └─ implementation DataGet.Connections.App.Services.ApiTokenService.GetTokenAsync [L11-L61]
        └─ uses_service ConnectionContextProvider
          └─ method GetTokenAsync [L28]
            └─ ... (no implementation details available)
        └─ uses_service IControlledRepository<ApiToken> (Scoped (inferred))
          └─ method GetTokenAsync [L26]
            └─ implementation DataGet.Data.Repository.Connections.ApiTokenRepository.GetTokenAsync
        └─ maps_to ExternalApiToken [L30]
  └─ impact_summary
    └─ services 1
      └─ IApiTokenService
    └─ mappings 1
      └─ ExternalApiToken

