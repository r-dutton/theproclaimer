[web] PUT /api/workpaper-record-templates/{id:Guid}/include/{include:bool}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.UpdateIncludes)  [L139–L158] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls WorkpaperRecordTemplateRepository.WriteQuery [L146]
  └─ write WorkpaperRecordTemplate [L146]
    └─ reads_from WorkpaperTemplates
  └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
    └─ method WriteQuery [L146]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L144]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

