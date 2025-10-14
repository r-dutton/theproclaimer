[web] GET /api/accounting/tax/taxonomy/report  (Cirrus.Api.Controllers.Accounting.Tax.TaxonomyController.GetReport)  [L47–L51] [auth=user]
  └─ sends_request GetTaxonomyReport [L50]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Tax.GetTaxonomyReportQueryHandler.Handle [L43–L148]
      └─ maps_to AccountLightListDto [L135]
      └─ maps_to AccountTypeLightWithTaxonomyDto [L112]
      └─ maps_to TaxonomyDto [L106]
      └─ uses_service GetAccountsQuery
        └─ method Execute [L79]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L78]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L66]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L109]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L71]

