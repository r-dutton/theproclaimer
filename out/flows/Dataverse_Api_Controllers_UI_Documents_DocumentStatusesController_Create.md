[web] POST /api/ui/documents/statuses  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.Create)  [L77–L90] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to DocumentStatusDto [L89]
  └─ calls DocumentStatusRepository.Add [L87]
  └─ calls DocumentStatusRepository.WriteQuery [L80]
  └─ writes_to DocumentStatus [L87]
    └─ reads_from DVS_DocumentStatuses
  └─ writes_to DocumentStatus [L80]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method WriteQuery [L80]
  └─ uses_service IMapper
    └─ method Map [L89]

