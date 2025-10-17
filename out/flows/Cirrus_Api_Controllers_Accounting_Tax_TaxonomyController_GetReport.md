[web] GET /api/accounting/tax/taxonomy/report  (Cirrus.Api.Controllers.Accounting.Tax.TaxonomyController.GetReport)  [L47–L51] status=200 [auth=user]
  └─ sends_request GetTaxonomyReport -> GetTaxonomyReportQueryHandler [L50]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Tax.GetTaxonomyReportQueryHandler.Handle [L43–L148]
      └─ maps_to AccountTypeLightWithTaxonomyDto [L112]
      └─ maps_to TaxonomyDto [L106]
      └─ maps_to AccountLightListDto [L135]
      └─ uses_service GetAccountsQuery
        └─ method Execute [L79]
          └─ resolves_request Cirrus.Connections.DataGet.Queries.GetAccountsQuery.Execute
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
          └─ resolves_request DataGet.Connections.App.Queries.GetAccountsQuery.Execute
          └─ resolves_request DataGet.Connections.External.Bgl360.Api.Queries.GetAccountsQuery.Execute
          └─ +4 additional_requests elided
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L78]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L71]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L66]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
  └─ impact_summary
    └─ requests 3
      └─ GetAccountsQuery
      └─ GetTaxonomyReport
      └─ GetTrialBalanceForDatesQuery
    └─ handlers 3
      └─ GetAccountsQueryHandler
      └─ GetTaxonomyReportQueryHandler
      └─ GetTrialBalanceForDatesQueryHandler
    └─ mappings 3
      └─ AccountLightListDto
      └─ AccountTypeLightWithTaxonomyDto
      └─ TaxonomyDto

