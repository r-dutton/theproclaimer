[web] PUT /api/workpaper-record-templates/archive-maps/{workpaperRecordTemplateId:guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.UpdateArchivedWorkpaperRecordTemplateMapping)  [L203–L216] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.WriteQuery [L209]
  └─ write ArchivedWorkpaperRecordTemplateMapping [L209]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ ArchivedWorkpaperRecordTemplateMapping writes=1 reads=0

