[web] PUT /api/ui/documents/statuses/{id:Guid}/reorder  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.Reorder)  [L104–L114] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentStatusRepository.WriteQuery [L107]
  └─ write DocumentStatus [L107]
    └─ reads_from DVS_DocumentStatuses
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DocumentStatus writes=1 reads=0

