[web] GET /api/worksheets/download  (Workpapers.Next.API.Controllers.WorksheetsController.Download)  [L83–L99] status=200
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L95]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UserService
    └─ method GetUser [L86]
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
  └─ uses_storage StorageService.CreateDownloadUrl [L95]
  └─ sends_request GetTemplateForDateTimeQuery -> GetTemplateForDateTimeQueryHandler [L86]
    └─ handled_by Workpapers.Next.Data.Queries.Templates.GetTemplateForDateTimeQueryHandler.Handle [L12–L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L23]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ services 2
      └─ StorageService
      └─ UserService
    └─ requests 1
      └─ GetTemplateForDateTimeQuery
    └─ handlers 1
      └─ GetTemplateForDateTimeQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ storages 1
      └─ StorageService

