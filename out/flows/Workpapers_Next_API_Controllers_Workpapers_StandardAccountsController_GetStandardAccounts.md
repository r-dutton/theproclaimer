[web] GET /api/standard-accounts  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.GetStandardAccounts)  [L67–L77] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to StandardAccountLightDto [L71]
    └─ automapper.registration WorkpapersMappingProfile (StandardAccount->StandardAccountLightDto) [L690]
  └─ calls StandardAccountRepository.ReadQuery [L71]
  └─ query StandardAccount [L71]
    └─ reads_from StandardAccounts
  └─ uses_service StandardAccountRepository
    └─ method ReadQuery [L71]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.StandardAccountRepository.ReadQuery [L9-L42]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ StandardAccount writes=0 reads=1
    └─ services 1
      └─ StandardAccountRepository
    └─ mappings 1
      └─ StandardAccountLightDto

