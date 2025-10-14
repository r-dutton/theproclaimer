[web] PUT /api/master-accounts/{id:int}/name-variants  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.UpdateMasterAccountNameVariants)  [L89–L97] [auth=AuthorizationPolicies.Administrator]
  └─ calls MasterAccountRepository.WriteQuery [L94]
  └─ writes_to MasterAccount [L94]
    └─ reads_from MasterAccounts
  └─ uses_service IControlledRepository<MasterAccount>
    └─ method WriteQuery [L94]

