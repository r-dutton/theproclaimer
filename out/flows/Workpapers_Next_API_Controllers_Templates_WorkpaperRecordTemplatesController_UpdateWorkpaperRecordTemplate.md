[web] PUT /api/workpaper-record-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.UpdateWorkpaperRecordTemplate)  [L116–L137] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls WorkpaperRecordTemplateRepository.WriteQuery [L121]
  └─ write WorkpaperRecordTemplate [L121]
    └─ reads_from WorkpaperTemplates
  └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
    └─ method WriteQuery [L121]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L123]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request UpdateWorkpaperRecordTemplateCommand [L134]

