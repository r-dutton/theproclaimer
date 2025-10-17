[web] GET /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Get)  [L52–L61] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to StandardAccountDto [L56]
    └─ automapper.registration WorkpapersMappingProfile (StandardAccount->StandardAccountDto) [L692]
  └─ calls StandardAccountRepository.ReadQuery [L56]
  └─ query StandardAccount [L56]
    └─ reads_from StandardAccounts
  └─ uses_service StandardAccountRepository
    └─ method ReadQuery [L56]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.StandardAccountRepository.ReadQuery [L9-L42]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardAccount writes=0 reads=1
    └─ services 1
      └─ StandardAccountRepository
    └─ mappings 1
      └─ StandardAccountDto

