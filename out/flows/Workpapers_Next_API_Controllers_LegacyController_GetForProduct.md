[web] GET /api/worksheets/UserWorksheets  (Workpapers.Next.API.Controllers.LegacyController.GetForProduct)  [L50–L64] status=200
  └─ maps_to LegacyDocumentDto [L61]
  └─ uses_service UserService
    └─ method GetFirmId [L53]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request AllWorkpaperRecordTemplatesForProductV2Query -> AllWorkpaperRecordTemplatesForProductQueryHandler [L58]
    └─ handled_by Workpapers.Next.Data.Queries.Templates.AllWorkpaperRecordTemplatesForProductQueryHandler.Handle [L18–L141]
      └─ maps_to WorkpaperRecordTemplateV2Dto [L88]
        └─ converts_to WorkpaperRecordTemplateV2Dto [L290]
        └─ converts_to LegacyDocumentDto [L343]
      └─ maps_to SectionWithWorkpaperRecordTemplateIdsDto [L54]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L69]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<ExcludedWorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L65]
          └─ implementation Workpapers.Next.Data.Repository.Templates.ExcludedWorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<Product> (Scoped (inferred))
        └─ method ReadQuery [L54]
          └─ implementation Workpapers.Next.Data.Repository.Licensing.ProductRepository.ReadQuery
  └─ sends_request GetProductQuery -> GetProductQueryHandler [L53]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 2
      └─ AllWorkpaperRecordTemplatesForProductV2Query
      └─ GetProductQuery
    └─ handlers 2
      └─ AllWorkpaperRecordTemplatesForProductQueryHandler
      └─ GetProductQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 3
      └─ LegacyDocumentDto
      └─ SectionWithWorkpaperRecordTemplateIdsDto
      └─ WorkpaperRecordTemplateV2Dto

