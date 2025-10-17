[web] GET /api/master-accounts/{id:int}  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.Get)  [L47–L55] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MasterAccountDto [L50]
    └─ automapper.registration WorkpapersMappingProfile (MasterAccount->MasterAccountDto) [L703]
  └─ calls MasterAccountRepository.ReadQuery [L50]
  └─ query MasterAccount [L50]
    └─ reads_from MasterAccounts
  └─ uses_service MasterAccountRepository
    └─ method ReadQuery [L50]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MasterAccountRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MasterAccount writes=0 reads=1
    └─ services 1
      └─ MasterAccountRepository
    └─ mappings 1
      └─ MasterAccountDto

