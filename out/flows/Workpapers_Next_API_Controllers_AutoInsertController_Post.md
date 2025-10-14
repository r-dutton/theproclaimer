[web] POST /api/autoinsert/suggestions  (Workpapers.Next.API.Controllers.AutoInsertController.Post)  [L32–L45]
  └─ uses_service UserService
    └─ method GetFirmId [L35]
  └─ sends_request GetAutoInsertSuggestionsQuery [L40]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.GetAutoInsertSuggestionsQueryHandler.Handle [L31–L131]
      └─ uses_service ArtificialIntelligenceService
        └─ method GetWeightedTemplatePredictions [L98]
      └─ uses_service RequestProcessor
        └─ method Process [L102]
      └─ uses_service UnitOfWork
        └─ method Table [L57]
      └─ uses_service UserService
        └─ method GetFirmId [L54]
  └─ sends_request GetProductQuery [L35]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]
  └─ sends_request GetFilteredSuggestionsQuery [L42]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetFilteredSuggestionsQueryHandler.Handle [L22–L52]
  └─ sends_request AllWorkpaperRecordTemplatesForProductV2Query [L37]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Queries.Templates.AllWorkpaperRecordTemplatesForProductQueryHandler.Handle [L18–L141]
      └─ maps_to SectionWithWorkpaperRecordTemplateIdsDto [L54]
      └─ maps_to WorkpaperRecordTemplateV2Dto [L88]
        └─ converts_to LegacyDocumentDto [L343]
        └─ converts_to WorkpaperRecordTemplateV2Dto [L290]
      └─ maps_to WorkpaperRecordTemplateV2Dto [L106]
        └─ converts_to LegacyDocumentDto [L343]
        └─ converts_to WorkpaperRecordTemplateV2Dto [L290]
      └─ uses_service IControlledRepository<ExcludedWorkpaperRecordTemplate>
        └─ method ReadQuery [L65]
      └─ uses_service IControlledRepository<Product>
        └─ method ReadQuery [L54]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L69]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L57]

