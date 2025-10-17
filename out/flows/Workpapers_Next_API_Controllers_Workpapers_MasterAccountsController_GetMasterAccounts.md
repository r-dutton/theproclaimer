[web] GET /api/master-accounts  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.GetMasterAccounts)  [L61–L70] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MasterAccountDto [L64]
    └─ automapper.registration WorkpapersMappingProfile (MasterAccount->MasterAccountDto) [L703]
  └─ calls MasterAccountRepository.ReadQuery [L64]
  └─ query MasterAccount [L64]
    └─ reads_from MasterAccounts
  └─ uses_service MasterAccountRepository
    └─ method ReadQuery [L64]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MasterAccountRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MasterAccount writes=0 reads=1
    └─ services 1
      └─ MasterAccountRepository
    └─ mappings 1
      └─ MasterAccountDto

