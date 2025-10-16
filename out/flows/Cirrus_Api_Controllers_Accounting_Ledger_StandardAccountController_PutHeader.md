[web] PUT /api/accounting/ledger/standard-accounts/header/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.PutHeader)  [L168–L179] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.WriteQuery [L171]
  └─ write StandardAccount [L171]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method WriteQuery [L171]
      └─ ... (no implementation details available)
  └─ sends_request CheckForExistingLeadScheduleInStandardAccountHierarchyQuery [L175]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CheckForExistingLeadScheduleInHierarchyQueryHandler.Handle [L31–L78]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L49]
          └─ ... (no implementation details available)
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method GetAccountsInHierarchy [L55]
          └─ implementation Cirrus.ApplicationService.Features.LeadSchedules.LeadScheduleService.GetAccountsInHierarchy [L14-L50]

