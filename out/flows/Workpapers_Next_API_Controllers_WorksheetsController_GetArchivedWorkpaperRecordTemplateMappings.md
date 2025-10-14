[web] GET /api/worksheets/archive-maps  (Workpapers.Next.API.Controllers.WorksheetsController.GetArchivedWorkpaperRecordTemplateMappings)  [L71–L78]
  └─ maps_to ArchivedWorkpaperRecordTemplateMappingLightDto [L74]
    └─ automapper.registration WorkpapersMappingProfile (ArchivedWorkpaperRecordTemplateMapping->ArchivedWorkpaperRecordTemplateMappingLightDto) [L364]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.ReadQuery [L74]
  └─ queries ArchivedWorkpaperRecordTemplateMapping [L74]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping>
    └─ method ReadQuery [L74]

