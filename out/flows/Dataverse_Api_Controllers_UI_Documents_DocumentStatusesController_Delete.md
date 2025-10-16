[web] DELETE /api/ui/documents/statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.Delete)  [L116–L126] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentStatusRepository.Remove [L123]
  └─ calls DocumentStatusRepository.WriteQuery [L119]
  └─ delete DocumentStatus [L123]
    └─ reads_from DVS_DocumentStatuses
  └─ write DocumentStatus [L119]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method WriteQuery [L119]
      └─ ... (no implementation details available)

