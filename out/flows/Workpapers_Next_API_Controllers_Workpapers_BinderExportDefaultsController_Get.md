[web] GET /api/binder-export-defaults/  (Workpapers.Next.API.Controllers.Workpapers.BinderExportDefaultsController.Get)  [L33–L41] status=200
  └─ maps_to BinderExportDefaultsDto [L36]
    └─ automapper.registration WorkpapersMappingProfile (BinderExportDefaults->BinderExportDefaultsDto) [L910]
  └─ calls BinderExportDefaultsRepository.ReadQuery [L36]
  └─ query BinderExportDefaults [L36]
    └─ reads_from BinderExportDefaults
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderExportDefaults writes=0 reads=1
    └─ mappings 1
      └─ BinderExportDefaultsDto

