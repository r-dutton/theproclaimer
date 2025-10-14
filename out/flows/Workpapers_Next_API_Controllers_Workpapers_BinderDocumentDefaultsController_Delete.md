[web] DELETE /api/binder-document-defaults/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderDocumentDefaultsController.Delete)  [L89–L99]
  └─ calls BinderDocumentDefaultsRepository.Remove [L96]
  └─ calls BinderDocumentDefaultsRepository.WriteQuery [L92]
  └─ writes_to BinderDocumentDefaults [L96]
    └─ reads_from BinderDocumentDefaults
  └─ writes_to BinderDocumentDefaults [L92]
    └─ reads_from BinderDocumentDefaults
  └─ uses_service IControlledRepository<BinderDocumentDefaults>
    └─ method WriteQuery [L92]

