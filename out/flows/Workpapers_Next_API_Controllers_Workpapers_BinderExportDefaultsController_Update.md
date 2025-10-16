[web] PUT /api/binder-export-defaults/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderExportDefaultsController.Update)  [L52–L62] status=200
  └─ calls BinderExportDefaultsRepository.WriteQuery [L55]
  └─ write BinderExportDefaults [L55]
    └─ reads_from BinderExportDefaults
  └─ uses_service IControlledRepository<BinderExportDefaults>
    └─ method WriteQuery [L55]
      └─ ... (no implementation details available)

