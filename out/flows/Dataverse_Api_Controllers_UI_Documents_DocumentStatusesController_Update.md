[web] PUT /api/ui/documents/statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.Update)  [L92–L102] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentStatusRepository.WriteQuery [L95]
  └─ write DocumentStatus [L95]
    └─ reads_from DVS_DocumentStatuses
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DocumentStatus writes=1 reads=0

