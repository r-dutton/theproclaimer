[web] PUT /api/accounting/ledger/standard-accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.PutHeader)  [L168–L179] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.WriteQuery [L171]
  └─ write StandardAccount [L171]
    └─ reads_from StandardAccounts
  └─ sends_request CheckForExistingLeadScheduleInStandardAccountHierarchyQuery -> CheckForExistingLeadScheduleInHierarchyQueryHandler [L175]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CheckForExistingLeadScheduleInHierarchyQueryHandler.Handle [L31–L78]
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method GetAccountsInHierarchy [L55]
          └─ implementation Cirrus.ApplicationService.Features.LeadSchedules.LeadScheduleService.GetAccountsInHierarchy [L14-L50]
            └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
              └─ method GetAccountsInHierarchy [L27]
                └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.GetAccountsInHierarchy
      └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
        └─ method ReadQuery [L49]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.ReadQuery
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ StandardAccount writes=1 reads=0
    └─ requests 1
      └─ CheckForExistingLeadScheduleInStandardAccountHierarchyQuery
    └─ handlers 1
      └─ CheckForExistingLeadScheduleInHierarchyQueryHandler

