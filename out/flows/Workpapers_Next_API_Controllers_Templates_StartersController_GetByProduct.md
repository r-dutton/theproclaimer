[web] GET /api/starters/byproduct/{ProductId:Guid}  (Workpapers.Next.API.Controllers.Templates.StartersController.GetByProduct)  [L61–L67] status=200
  └─ maps_to StarterDto [L66]
  └─ uses_service UserService
    └─ method GetUser [L64]
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
  └─ sends_request AllStartersForProductQuery -> AllStartersForProductQueryHandler [L64]
    └─ handled_by Workpapers.Next.Data.Queries.Templates.Starters.AllStartersForProductQueryHandler.Handle [L17–L63]
      └─ uses_service UnitOfWork
        └─ method Table [L40]
          └─ implementation UnitOfWork.Table
      └─ uses_service UserService
        └─ method IsSuperUser [L35]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
            └─ uses_service bool?
              └─ method IsSuperUser [L174]
                └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L35]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ AllStartersForProductQuery
    └─ handlers 1
      └─ AllStartersForProductQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ StarterDto

