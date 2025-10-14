[web] GET /api/standard-accounts  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.GetStandardAccounts)  [L67–L77] [auth=AuthorizationPolicies.User]
  └─ maps_to StandardAccountLightDto [L71]
    └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountLightDto) [L445]
    └─ automapper.registration WorkpapersMappingProfile (StandardAccount->StandardAccountLightDto) [L690]
  └─ calls StandardAccountRepository.ReadQuery [L71]
  └─ queries StandardAccount [L71]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method ReadQuery [L71]

