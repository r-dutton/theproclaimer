[web] DELETE /api/workpaper-record-templates/hard-delete/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.Delete)  [L160–L191] [auth=AuthorizationPolicies.Administrator]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.Add [L186]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.WriteQuery [L175]
  └─ calls WorkpaperRecordTemplateRepository.Remove [L188]
  └─ calls WorkpaperRecordTemplateRepository.WriteQuery [L165]
  └─ writes_to ArchivedWorkpaperRecordTemplateMapping [L186]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ writes_to ArchivedWorkpaperRecordTemplateMapping [L175]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ writes_to WorkpaperRecordTemplate [L188]
    └─ reads_from WorkpaperTemplates
  └─ writes_to WorkpaperRecordTemplate [L165]
    └─ reads_from WorkpaperTemplates
  └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping>
    └─ method WriteQuery [L175]
  └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
    └─ method WriteQuery [L165]

