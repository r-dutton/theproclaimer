[web] PUT /api/workpaper-record-templates/archive-maps/{workpaperRecordTemplateId:guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.UpdateArchivedWorkpaperRecordTemplateMapping)  [L203–L216] [auth=AuthorizationPolicies.SuperUser]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.WriteQuery [L209]
  └─ writes_to ArchivedWorkpaperRecordTemplateMapping [L209]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping>
    └─ method WriteQuery [L209]

