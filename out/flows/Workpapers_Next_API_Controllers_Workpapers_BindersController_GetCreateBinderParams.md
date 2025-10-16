[web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.GetCreateBinderParams)  [L541–L566] status=200
  └─ calls BinderTemplateRepository.ReadQuery [L543]
  └─ query BinderTemplate [L543]
    └─ reads_from BinderTemplates
  └─ uses_service UserService
    └─ method GetUser [L565]
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
  └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
    └─ method WriteQuery [L556]
      └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderTemplate writes=0 reads=1
    └─ services 2
      └─ IControlledRepository<RecordStatus>
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

