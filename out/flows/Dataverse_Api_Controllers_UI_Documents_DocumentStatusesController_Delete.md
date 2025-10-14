[web] DELETE /api/ui/documents/statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.Delete)  [L116–L126] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentStatusRepository.Remove [L123]
  └─ calls DocumentStatusRepository.WriteQuery [L119]
  └─ writes_to DocumentStatus [L123]
    └─ reads_from DVS_DocumentStatuses
  └─ writes_to DocumentStatus [L119]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method WriteQuery [L119]

