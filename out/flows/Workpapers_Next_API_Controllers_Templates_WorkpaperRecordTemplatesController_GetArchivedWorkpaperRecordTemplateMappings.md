[web] GET /api/workpaper-record-templates/archive-maps  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.GetArchivedWorkpaperRecordTemplateMappings)  [L193–L201] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to ArchivedWorkpaperRecordTemplateMappingDto [L198]
    └─ automapper.registration WorkpapersMappingProfile (ArchivedWorkpaperRecordTemplateMapping->ArchivedWorkpaperRecordTemplateMappingDto) [L365]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.ReadQuery [L198]
  └─ query ArchivedWorkpaperRecordTemplateMapping [L198]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ArchivedWorkpaperRecordTemplateMapping writes=0 reads=1
    └─ mappings 1
      └─ ArchivedWorkpaperRecordTemplateMappingDto

