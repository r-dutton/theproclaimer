[web] PUT /api/internal/messages/{messageId:guid}/bulk-mark-as-sent  (Dataverse.Api.Controllers.Internal.MessagesController.BulkMarkAsSent)  [L56–L69] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls MessageRepository.WriteQuery [L59]
  └─ write Message [L59]
    └─ reads_from Messages
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Message writes=1 reads=0

