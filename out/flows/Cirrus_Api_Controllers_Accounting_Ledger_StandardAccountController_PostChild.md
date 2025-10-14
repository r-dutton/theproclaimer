[web] POST /api/accounting/ledger/standard-accounts/child  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.PostChild)  [L108–L118] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.Add [L116]
  └─ writes_to StandardAccount [L116]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method Add [L116]
  └─ sends_request GetStandardAccountManagerQuery [L112]
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

