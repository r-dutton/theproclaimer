[web] GET /api/sources/export/{id:guid}/journal-for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetJournal)  [L61–L66] [auth=AuthorizationPolicies.User]
  └─ sends_request GetExportSourceJournalQuery [L64]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourceJournalQueryHandler.Handle [L38–L265]
      └─ maps_to SourceAccountUltraLightDto [L139]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountUltraLightDto) [L260]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountUltraLightDto) [L615]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L90]
      └─ uses_service IControlledRepository<ExportSource>
        └─ method ReadQuery [L69]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L108]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L141]
      └─ uses_service ISwingingAccountService (AddScoped)
        └─ method GetSwingingAccounts [L80]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L106]

