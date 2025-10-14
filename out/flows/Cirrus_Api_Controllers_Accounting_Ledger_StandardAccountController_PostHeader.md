[web] POST /api/accounting/ledger/standard-accounts/header  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.PostHeader)  [L146–L163] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.Add [L161]
  └─ writes_to StandardAccount [L161]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method Add [L161]
  └─ sends_request CheckForExistingLeadScheduleInStandardAccountHierarchyQuery [L151]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CheckForExistingLeadScheduleInHierarchyQueryHandler.Handle [L31–L78]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L49]
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method GetAccountsInHierarchy [L55]
  └─ sends_request GetStandardAccountManagerQuery [L157]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetStandardAccountManagerQueryHandler.Handle [L54–L184]
      └─ maps_to MasterAccountForStandardChartDto [L179]
        └─ automapper.registration CirrusMappingProfile (MasterAccount->MasterAccountForStandardChartDto) [L306]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method WriteQuery [L98]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L83]
      └─ uses_service ILogger
        └─ method LogWarning [L142]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L181]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L105]

