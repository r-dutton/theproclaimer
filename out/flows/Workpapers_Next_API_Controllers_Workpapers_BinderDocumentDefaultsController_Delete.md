[web] DELETE /api/binder-document-defaults/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderDocumentDefaultsController.Delete)  [L89–L99] status=200
  └─ calls BinderDocumentDefaultsRepository.Remove [L96]
  └─ calls BinderDocumentDefaultsRepository.WriteQuery [L92]
  └─ delete BinderDocumentDefaults [L96]
    └─ reads_from BinderDocumentDefaults
  └─ write BinderDocumentDefaults [L92]
    └─ reads_from BinderDocumentDefaults
  └─ uses_service IControlledRepository<BinderDocumentDefaults>
    └─ method WriteQuery [L92]
      └─ ... (no implementation details available)

