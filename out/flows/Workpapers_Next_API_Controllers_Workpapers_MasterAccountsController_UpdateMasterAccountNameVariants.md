[web] PUT /api/master-accounts/{id:int}/name-variants  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.UpdateMasterAccountNameVariants)  [L89–L97] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MasterAccountRepository.WriteQuery [L94]
  └─ write MasterAccount [L94]
    └─ reads_from MasterAccounts
  └─ uses_service MasterAccountRepository
    └─ method WriteQuery [L94]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MasterAccountRepository.WriteQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ MasterAccount writes=1 reads=0
    └─ services 1
      └─ MasterAccountRepository

