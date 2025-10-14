[web] PUT /api/workpaper-record-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.UpdateWorkpaperRecordTemplate)  [L116–L137] [auth=AuthorizationPolicies.Administrator]
  └─ calls WorkpaperRecordTemplateRepository.WriteQuery [L121]
  └─ writes_to WorkpaperRecordTemplate [L121]
    └─ reads_from WorkpaperTemplates
  └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
    └─ method WriteQuery [L121]
  └─ uses_service UserService
    └─ method IsSuperUser [L123]
  └─ sends_request UpdateWorkpaperRecordTemplateCommand [L134]

