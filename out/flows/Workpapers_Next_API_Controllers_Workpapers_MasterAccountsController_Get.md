[web] GET /api/master-accounts/{id:int}  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.Get)  [L47–L55] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MasterAccountDto [L50]
    └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountDto) [L301]
    └─ automapper.registration WorkpapersMappingProfile (MasterAccount->MasterAccountDto) [L703]
  └─ calls MasterAccountRepository.ReadQuery [L50]
  └─ queries MasterAccount [L50]
    └─ reads_from MasterAccounts
  └─ uses_service IControlledRepository<MasterAccount>
    └─ method ReadQuery [L50]

