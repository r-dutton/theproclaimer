[web] POST /api/accounting/ledger/standard-accounts/child  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.PostChild)  [L108–L118] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.Add [L116]
  └─ insert StandardAccount [L116]
    └─ reads_from StandardAccounts
  └─ uses_service IControlledRepository<StandardAccount>
    └─ method Add [L116]
      └─ ... (no implementation details available)
  └─ sends_request GetStandardAccountManagerQuery [L112]
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

