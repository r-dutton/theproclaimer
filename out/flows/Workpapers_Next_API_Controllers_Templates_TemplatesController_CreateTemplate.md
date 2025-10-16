[web] POST /api/templates  (Workpapers.Next.API.Controllers.Templates.TemplatesController.CreateTemplate)  [L79–L92] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TemplateDto [L91]
  └─ uses_service UnitOfWork
    └─ method Add [L89]
      └─ implementation UnitOfWork.Add
  └─ uses_service UserService
    └─ method GetUserId [L87]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ TemplateDto

