[web] POST /api/workpaper-record-templates  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.CreateWorkpaperRecordTemplate)  [L99–L114] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L106]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request CreateWorkpaperRecordTemplateCommand [L111]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.WorkpaperTemplates.CreateOrUpdateWorkpaperRecordTemplateCommandHandler.Handle [L43–L91]
      └─ uses_service UserService
        └─ method GetUserId [L78]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service UnitOfWork
        └─ method Add [L61]
          └─ ... (no implementation details available)

