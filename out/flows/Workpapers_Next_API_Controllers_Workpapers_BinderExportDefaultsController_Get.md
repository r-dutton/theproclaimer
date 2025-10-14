[web] GET /api/binder-export-defaults/  (Workpapers.Next.API.Controllers.Workpapers.BinderExportDefaultsController.Get)  [L33–L41]
  └─ maps_to BinderExportDefaultsDto [L36]
    └─ automapper.registration WorkpapersMappingProfile (BinderExportDefaults->BinderExportDefaultsDto) [L910]
  └─ calls BinderExportDefaultsRepository.ReadQuery [L36]
  └─ queries BinderExportDefaults [L36]
    └─ reads_from BinderExportDefaults
  └─ uses_service IControlledRepository<BinderExportDefaults>
    └─ method ReadQuery [L36]

