[web] GET /api/binder-document-defaults/  (Workpapers.Next.API.Controllers.Workpapers.BinderDocumentDefaultsController.Get)  [L50–L65]
  └─ maps_to BinderDocumentDefaultsDto [L53]
    └─ automapper.registration WorkpapersMappingProfile (BinderDocumentDefaults->BinderDocumentDefaultsDto) [L897]
  └─ calls BinderDocumentDefaultsRepository.ReadQuery [L53]
  └─ queries BinderDocumentDefaults [L53]
    └─ reads_from BinderDocumentDefaults
  └─ uses_service IControlledRepository<BinderDocumentDefaults>
    └─ method ReadQuery [L53]

