[web] PUT /api/ui/documents/statuses/{id:Guid}/reorder  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.Reorder)  [L104–L114] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentStatusRepository.WriteQuery [L107]
  └─ writes_to DocumentStatus [L107]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method WriteQuery [L107]

