[web] DELETE /api/workpaper-record-templates/hard-delete/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.Delete)  [L160–L191] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls WorkpaperRecordTemplateRepository.Remove [L188]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository (methods: Add,WriteQuery) [L186]
  └─ calls WorkpaperRecordTemplateRepository.WriteQuery [L165]
  └─ delete WorkpaperRecordTemplate [L188]
    └─ reads_from WorkpaperTemplates
  └─ insert ArchivedWorkpaperRecordTemplateMapping [L186]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ write ArchivedWorkpaperRecordTemplateMapping [L175]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ write WorkpaperRecordTemplate [L165]
    └─ reads_from WorkpaperTemplates
  └─ impact_summary
    └─ entities 2 (writes=4, reads=0)
      └─ ArchivedWorkpaperRecordTemplateMapping writes=2 reads=0
      └─ WorkpaperRecordTemplate writes=2 reads=0

