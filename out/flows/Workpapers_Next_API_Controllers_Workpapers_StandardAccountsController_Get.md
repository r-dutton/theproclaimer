[web] GET /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Get)  [L52–L61] [auth=AuthorizationPolicies.User]
  └─ maps_to StandardAccountDto [L56]
    └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountDto) [L436]
    └─ automapper.registration WorkpapersMappingProfile (StandardAccount->StandardAccountDto) [L692]
  └─ calls StandardAccountRepository.ReadQuery [L56]
  └─ queries StandardAccount [L56]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method ReadQuery [L56]

