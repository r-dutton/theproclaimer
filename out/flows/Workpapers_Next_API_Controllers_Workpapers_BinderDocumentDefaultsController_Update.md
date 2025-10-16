[web] PUT /api/binder-document-defaults/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderDocumentDefaultsController.Update)  [L77–L87] status=200
  └─ calls BinderDocumentDefaultsRepository.WriteQuery [L80]
  └─ write BinderDocumentDefaults [L80]
    └─ reads_from BinderDocumentDefaults
  └─ uses_service IControlledRepository<BinderDocumentDefaults>
    └─ method WriteQuery [L80]
      └─ ... (no implementation details available)

