[web] GET /api/worksheets/archive-maps  (Workpapers.Next.API.Controllers.WorksheetsController.GetArchivedWorkpaperRecordTemplateMappings)  [L71–L78] status=200
  └─ maps_to ArchivedWorkpaperRecordTemplateMappingLightDto [L74]
    └─ automapper.registration WorkpapersMappingProfile (ArchivedWorkpaperRecordTemplateMapping->ArchivedWorkpaperRecordTemplateMappingLightDto) [L364]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.ReadQuery [L74]
  └─ query ArchivedWorkpaperRecordTemplateMapping [L74]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ArchivedWorkpaperRecordTemplateMapping writes=0 reads=1
    └─ mappings 1
      └─ ArchivedWorkpaperRecordTemplateMappingLightDto

