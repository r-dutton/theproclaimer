[web] POST /api/accounting/ledger/standard-accounts/header  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.PostHeader)  [L146–L163] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.Add [L161]
  └─ insert StandardAccount [L161]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method Add [L161]
      └─ ... (no implementation details available)
  └─ sends_request CheckForExistingLeadScheduleInStandardAccountHierarchyQuery [L151]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CheckForExistingLeadScheduleInHierarchyQueryHandler.Handle [L31–L78]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L49]
          └─ ... (no implementation details available)
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method GetAccountsInHierarchy [L55]
          └─ implementation Cirrus.ApplicationService.Features.LeadSchedules.LeadScheduleService.GetAccountsInHierarchy [L14-L50]
  └─ sends_request GetStandardAccountManagerQuery [L157]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetStandardAccountManagerQueryHandler.Handle [L54–L184]
      └─ maps_to MasterAccountForStandardChartDto [L179]
        └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountForStandardChartDto) [L306]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method WriteQuery [L98]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L83]
          └─ ... (no implementation details available)
      └─ uses_service ILogger
        └─ method LogWarning [L142]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L181]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L105]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ logs ILogger [Warning] [L142]

