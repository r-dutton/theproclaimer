[web] POST /api/ui/documents/statuses  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.Create)  [L77–L90] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to DocumentStatusDto [L89]
  └─ calls DocumentStatusRepository (methods: Add,WriteQuery) [L87]
  └─ insert DocumentStatus [L87]
    └─ reads_from DVS_DocumentStatuses
  └─ write DocumentStatus [L80]
    └─ reads_from DVS_DocumentStatuses
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ DocumentStatus writes=2 reads=0
    └─ mappings 1
      └─ DocumentStatusDto

