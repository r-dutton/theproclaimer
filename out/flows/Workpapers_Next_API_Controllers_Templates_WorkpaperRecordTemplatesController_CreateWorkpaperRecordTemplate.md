[web] POST /api/workpaper-record-templates  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.CreateWorkpaperRecordTemplate)  [L99–L114] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L106]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request CreateWorkpaperRecordTemplateCommand -> CreateOrUpdateWorkpaperRecordTemplateCommandHandler [L111]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.WorkpaperTemplates.CreateOrUpdateWorkpaperRecordTemplateCommandHandler.Handle [L43–L91]
      └─ uses_service UserService
        └─ method GetUserId [L78]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Add [L61]
          └─ implementation UnitOfWork.Add
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ CreateWorkpaperRecordTemplateCommand
    └─ handlers 1
      └─ CreateOrUpdateWorkpaperRecordTemplateCommandHandler
    └─ caches 1
      └─ IMemoryCache

