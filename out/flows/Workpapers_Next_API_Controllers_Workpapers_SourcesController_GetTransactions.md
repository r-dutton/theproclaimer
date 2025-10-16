[web] GET /api/sources/transactions  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetTransactions)  [L493–L529] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L498]
  └─ query Binder [L498]
    └─ reads_from Binders
  └─ sends_request GetGeneralLedgerBySourceAccountQuery -> GetGeneralLedgerBySourceAccountQueryHandler [L518]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerBySourceAccountQueryHandler.Handle [L54–L242]
      └─ maps_to SourceAccountMappingDto [L201]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
      └─ maps_to SourceLightDto [L181]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L206]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L186]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L149]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
              └─ calls SourceAccountRepository.ReadQuery [L151]
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L136]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L121]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service GetGeneralLedgerForDatesQuery
        └─ method Execute [L79]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetGeneralLedgerForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetGeneralLedgerForDatesQueryHandler.Handle [L38–L87]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1
    └─ requests 3
      └─ GetGeneralLedgerBySourceAccountQuery
      └─ GetGeneralLedgerForDatesQuery
      └─ GetTrialBalanceForDatesQuery
    └─ handlers 3
      └─ GetGeneralLedgerBySourceAccountQueryHandler
      └─ GetGeneralLedgerForDatesQueryHandler
      └─ GetTrialBalanceForDatesQueryHandler
    └─ mappings 2
      └─ SourceAccountMappingDto
      └─ SourceLightDto

