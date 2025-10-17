[web] POST /api/accounting/ledger/standard-accounts/child  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.PostChild)  [L108–L118] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls StandardAccountRepository.Add [L116]
  └─ insert StandardAccount [L116]
    └─ reads_from StandardAccounts
  └─ sends_request GetStandardAccountManagerQuery -> GetStandardAccountManagerQueryHandler [L112]
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
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ StandardAccount writes=1 reads=0
    └─ requests 1
      └─ GetStandardAccountManagerQuery
    └─ handlers 1
      └─ GetStandardAccountManagerQueryHandler
    └─ mappings 1
      └─ MasterAccountForStandardChartDto

