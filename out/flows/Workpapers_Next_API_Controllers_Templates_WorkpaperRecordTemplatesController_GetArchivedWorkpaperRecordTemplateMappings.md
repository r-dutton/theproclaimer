[web] GET /api/workpaper-record-templates/archive-maps  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.GetArchivedWorkpaperRecordTemplateMappings)  [L193–L201] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to ArchivedWorkpaperRecordTemplateMappingDto [L198]
    └─ automapper.registration WorkpapersMappingProfile (ArchivedWorkpaperRecordTemplateMapping->ArchivedWorkpaperRecordTemplateMappingDto) [L365]
  └─ calls ArchivedWorkpaperRecordTemplateMappingRepository.ReadQuery [L198]
  └─ queries ArchivedWorkpaperRecordTemplateMapping [L198]
    └─ reads_from ArchivedWorkpaperRecordTemplateMappings
  └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping>
    └─ method ReadQuery [L198]

