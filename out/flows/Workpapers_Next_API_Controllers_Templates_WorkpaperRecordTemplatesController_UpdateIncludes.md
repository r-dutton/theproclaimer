[web] PUT /api/workpaper-record-templates/{id:Guid}/include/{include:bool}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.UpdateIncludes)  [L139–L158] [auth=AuthorizationPolicies.Administrator]
  └─ calls WorkpaperRecordTemplateRepository.WriteQuery [L146]
  └─ writes_to WorkpaperRecordTemplate [L146]
    └─ reads_from WorkpaperTemplates
  └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
    └─ method WriteQuery [L146]
  └─ uses_service UserService
    └─ method GetFirmId [L144]

