[web] DELETE /api/ui/documents/statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.Delete)  [L116–L126] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DocumentStatusRepository (methods: Remove,WriteQuery) [L123]
  └─ delete DocumentStatus [L123]
    └─ reads_from DVS_DocumentStatuses
  └─ write DocumentStatus [L119]
    └─ reads_from DVS_DocumentStatuses
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ DocumentStatus writes=2 reads=0

