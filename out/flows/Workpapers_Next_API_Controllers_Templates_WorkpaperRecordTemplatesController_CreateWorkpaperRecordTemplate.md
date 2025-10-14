[web] POST /api/workpaper-record-templates  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.CreateWorkpaperRecordTemplate)  [L99–L114] [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L106]
  └─ sends_request CreateWorkpaperRecordTemplateCommand [L111]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.WorkpaperTemplates.CreateOrUpdateWorkpaperRecordTemplateCommandHandler.Handle [L43–L91]
      └─ uses_service UnitOfWork
        └─ method Add [L61]
      └─ uses_service UserService
        └─ method GetUserId [L78]

