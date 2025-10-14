[web] PUT /api/binder-document-defaults/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderDocumentDefaultsController.Update)  [L77–L87]
  └─ calls BinderDocumentDefaultsRepository.WriteQuery [L80]
  └─ writes_to BinderDocumentDefaults [L80]
    └─ reads_from BinderDocumentDefaults
  └─ uses_service IControlledRepository<BinderDocumentDefaults>
    └─ method WriteQuery [L80]

