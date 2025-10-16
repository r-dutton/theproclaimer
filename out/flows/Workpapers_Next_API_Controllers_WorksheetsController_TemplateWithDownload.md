[web] GET /api/worksheets/templatewithdownload  (Workpapers.Next.API.Controllers.WorksheetsController.TemplateWithDownload)  [L104–L122] status=200
  └─ maps_to WorkpaperRecordTemplateV2Dto [L118]
    └─ converts_to WorkpaperRecordTemplateV2Dto [L290]
    └─ converts_to LegacyDocumentDto [L343]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L119]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UnitOfWork
    └─ method Table [L118]
      └─ implementation UnitOfWork.Table
  └─ uses_service UserService
    └─ method GetUser [L107]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
        └─ uses_service RequestProcessor
          └─ method GetUserByDataverseId [L287]
            └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ +1 additional_requests elided
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_service User>
          └─ method GetUserIdFromToken [L106]
            └─ ... (no implementation details available)
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_storage StorageService.CreateDownloadUrl [L119]
  └─ sends_request GetTemplateForDateTimeQuery -> GetTemplateForDateTimeQueryHandler [L107]
    └─ handled_by Workpapers.Next.Data.Queries.Templates.GetTemplateForDateTimeQueryHandler.Handle [L12–L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L23]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ services 3
      └─ StorageService
      └─ UnitOfWork
      └─ UserService
    └─ requests 1
      └─ GetTemplateForDateTimeQuery
    └─ handlers 1
      └─ GetTemplateForDateTimeQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ storages 1
      └─ StorageService
    └─ mappings 1
      └─ WorkpaperRecordTemplateV2Dto

