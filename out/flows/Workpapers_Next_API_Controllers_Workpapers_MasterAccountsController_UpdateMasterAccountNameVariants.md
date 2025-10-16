[web] PUT /api/master-accounts/{id:int}/name-variants  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.UpdateMasterAccountNameVariants)  [L89–L97] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MasterAccountRepository.WriteQuery [L94]
  └─ write MasterAccount [L94]
    └─ reads_from MasterAccounts
  └─ uses_service IControlledRepository<MasterAccount>
    └─ method WriteQuery [L94]
      └─ ... (no implementation details available)

