[web] PUT /api/binder-export-defaults/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderExportDefaultsController.Update)  [L52–L62]
  └─ calls BinderExportDefaultsRepository.WriteQuery [L55]
  └─ writes_to BinderExportDefaults [L55]
    └─ reads_from BinderExportDefaults
  └─ uses_service IControlledRepository<BinderExportDefaults>
    └─ method WriteQuery [L55]

