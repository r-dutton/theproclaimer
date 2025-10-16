[web] POST /api/accounting/ledger/standard-accounts/header  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.PostHeader)  [L146–L163] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.Add [L161]
  └─ insert StandardAccount [L161]
    └─ reads_from StandardAccounts
  └─ sends_request GetStandardAccountManagerQuery -> GetStandardAccountManagerQueryHandler [L157]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetStandardAccountManagerQueryHandler.Handle [L54–L184]
      └─ maps_to MasterAccountForStandardChartDto [L179]
        └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountForStandardChartDto) [L306]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L105]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<MasterAccount> (Scoped (inferred))
        └─ method WriteQuery [L98]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.MasterAccountRepository.WriteQuery
      └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
        └─ method WriteQuery [L83]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.WriteQuery
      └─ logs ILogger [Warning] [L142]
  └─ sends_request CheckForExistingLeadScheduleInStandardAccountHierarchyQuery -> CheckForExistingLeadScheduleInHierarchyQueryHandler [L151]
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
    └─ requests 2
      └─ CheckForExistingLeadScheduleInStandardAccountHierarchyQuery
      └─ GetStandardAccountManagerQuery
    └─ handlers 2
      └─ CheckForExistingLeadScheduleInHierarchyQueryHandler
      └─ GetStandardAccountManagerQueryHandler
    └─ mappings 1
      └─ MasterAccountForStandardChartDto

