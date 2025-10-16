[web] GET /api/master-accounts  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.GetMasterAccounts)  [L61–L70] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MasterAccountDto [L64]
    └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountDto) [L301]
    └─ automapper.registration WorkpapersMappingProfile (MasterAccount->MasterAccountDto) [L703]
  └─ calls MasterAccountRepository.ReadQuery [L64]
  └─ query MasterAccount [L64]
    └─ reads_from MasterAccounts
  └─ uses_service IControlledRepository<MasterAccount>
    └─ method ReadQuery [L64]
      └─ ... (no implementation details available)

