[web] GET /api/worksheets/UserWorksheets  (Workpapers.Next.API.Controllers.LegacyController.GetForProduct)  [L50–L64]
  └─ maps_to LegacyDocumentDto [L61]
  └─ uses_service IMapper
    └─ method Map [L61]
  └─ uses_service UserService
    └─ method GetFirmId [L53]
  └─ sends_request GetProductQuery [L53]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]
  └─ sends_request AllWorkpaperRecordTemplatesForProductV2Query [L58]
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

