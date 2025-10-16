[web] PUT /api/workpaper-record-templates/archive-maps/{workpaperRecordTemplateId:guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.UpdateArchivedWorkpaperRecordTemplateMapping)  [L203–L216] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.WriteQuery [L209]
  └─ write ArchivedWorkpaperRecordTemplateMapping [L209]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping>
    └─ method WriteQuery [L209]
      └─ ... (no implementation details available)

