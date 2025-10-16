[web] GET /api/sources/transactions  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetTransactions)  [L493–L529] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L498]
  └─ query Binder [L498]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L498]
      └─ ... (no implementation details available)
  └─ sends_request GetGeneralLedgerBySourceAccountQuery [L518]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerBySourceAccountQueryHandler.Handle [L54–L242]
      └─ maps_to SourceAccountMappingDto [L201]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountMappingDto) [L613]
      └─ maps_to SourceLightDto [L181]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L186]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service GetGeneralLedgerForDatesQuery
        └─ method Execute [L79]
          └─ ... (no implementation details available)
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L149]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L121]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L136]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L184]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L206]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

