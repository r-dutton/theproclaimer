[web] GET /api/sources/transactions  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetTransactions)  [L493–L529] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L498]
  └─ queries Binder [L498]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L498]
  └─ sends_request GetGeneralLedgerBySourceAccountQuery [L518]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetGeneralLedgerBySourceAccountQueryHandler.Handle [L54–L242]
      └─ maps_to SourceAccountMappingDto [L201]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountMappingDto) [L613]
      └─ maps_to SourceLightDto [L181]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L186]
      └─ uses_service GetGeneralLedgerForDatesQuery
        └─ method Execute [L79]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L149]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L121]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L136]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L184]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L206]

