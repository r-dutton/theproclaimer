[web] GET /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Get)  [L52–L61] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to StandardAccountDto [L56]
    └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountDto) [L436]
    └─ automapper.registration WorkpapersMappingProfile (StandardAccount->StandardAccountDto) [L692]
  └─ calls StandardAccountRepository.ReadQuery [L56]
  └─ query StandardAccount [L56]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method ReadQuery [L56]
      └─ ... (no implementation details available)

