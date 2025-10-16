[web] PUT /api/ui/documents/statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.Update)  [L92–L102] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentStatusRepository.WriteQuery [L95]
  └─ write DocumentStatus [L95]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method WriteQuery [L95]
      └─ ... (no implementation details available)

