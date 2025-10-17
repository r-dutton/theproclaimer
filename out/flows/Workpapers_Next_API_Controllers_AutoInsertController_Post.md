[web] POST /api/autoinsert/suggestions  (Workpapers.Next.API.Controllers.AutoInsertController.Post)  [L32–L45] status=201
  └─ uses_service UserService
    └─ method GetFirmId [L35]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request GetFilteredSuggestionsQuery -> GetFilteredSuggestionsQueryHandler [L42]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetFilteredSuggestionsQueryHandler.Handle [L22–L52]
  └─ sends_request GetAutoInsertSuggestionsQuery -> GetAutoInsertSuggestionsQueryHandler [L40]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.GetAutoInsertSuggestionsQueryHandler.Handle [L31–L131]
      └─ uses_service RequestProcessor
        └─ method Process [L102]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.Process
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.Process
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.Process
          └─ +1 additional_requests elided
      └─ uses_service ArtificialIntelligenceService
        └─ method GetWeightedTemplatePredictions [L98]
          └─ implementation Workpapers.Next.ApplicationService.Features.ArtificialIntelligence.ArtificialIntelligenceService.GetWeightedTemplatePredictions [L12-L60]
            └─ uses_service ArtificialIntelligenceHmacService
              └─ method GetWeightedTemplatePredictions [L49]
                └─ implementation Workpapers.Next.ApplicationService.Features.ArtificialIntelligence.ArtificialIntelligenceHmacService.GetWeightedTemplatePredictions [L11-L28]
      └─ uses_service UnitOfWork
        └─ method Table [L57]
          └─ implementation UnitOfWork.Table
      └─ uses_service UserService
        └─ method GetFirmId [L54]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId (see previous expansion)
  └─ sends_request AllWorkpaperRecordTemplatesForProductV2Query -> AllWorkpaperRecordTemplatesForProductQueryHandler [L37]
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
  └─ sends_request GetProductQuery -> GetProductQueryHandler [L35]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]
          └─ implementation UnitOfWork.Table (see previous expansion)
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 4
      └─ AllWorkpaperRecordTemplatesForProductV2Query
      └─ GetAutoInsertSuggestionsQuery
      └─ GetFilteredSuggestionsQuery
      └─ GetProductQuery
    └─ handlers 4
      └─ AllWorkpaperRecordTemplatesForProductQueryHandler
      └─ GetAutoInsertSuggestionsQueryHandler
      └─ GetFilteredSuggestionsQueryHandler
      └─ GetProductQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ SectionWithWorkpaperRecordTemplateIdsDto
      └─ WorkpaperRecordTemplateV2Dto

