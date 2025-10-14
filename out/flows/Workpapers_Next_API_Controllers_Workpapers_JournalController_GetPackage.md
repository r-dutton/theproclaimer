[web] GET /api/journals/package  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetPackage)  [L186–L191] [auth=AuthorizationPolicies.User]
  └─ sends_request GetJournalPackageQuery [L190]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalPackageQueryHandler.Handle [L56–L244]
      └─ maps_to AccountLightListForJournalDto [L115]
        └─ automapper.registration CirrusMappingProfile (Account->AccountLightListForJournalDto) [L318]
      └─ maps_to StandardAccountLightListForJournalDto [L155]
        └─ automapper.registration CirrusMappingProfile (StandardAccount->StandardAccountLightListForJournalDto) [L447]
      └─ maps_to CachedSourceAccountDto [L134]
        └─ automapper.registration CirrusMappingProfile (CachedSourceAccount->CachedSourceAccountDto) [L287]
      └─ maps_to DivisionLightDto [L166]
        └─ automapper.registration CirrusMappingProfile (Division->DivisionLightDto) [L226]
      └─ maps_to SourceAccountLightDto [L124]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountLightDto) [L262]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountLightDto) [L617]
        └─ converts_to SourceAccountInJournalModel [L269]
        └─ converts_to LinkedSourceAccount [L72]
        └─ converts_to MappingSourceAccountModel [L830]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L115]
      └─ uses_service IControlledRepository<CachedSourceAccount>
        └─ method ReadQuery [L134]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L99]
      └─ uses_service IControlledRepository<Division>
        └─ method ReadQuery [L166]
      └─ uses_service IControlledRepository<Journal>
        └─ method ReadQuery [L177]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L124]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L155]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L121]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L95]
  └─ returns JournalPackageDto [L190] [return]

