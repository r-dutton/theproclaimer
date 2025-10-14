[web] PUT /api/internal/messages/{messageId:guid}/bulk-mark-as-sent  (Dataverse.Api.Controllers.Internal.MessagesController.BulkMarkAsSent)  [L56–L69] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls MessageRepository.WriteQuery [L59]
  └─ writes_to Message [L59]
    └─ reads_from Messages
  └─ uses_service IControlledRepository<Message>
    └─ method WriteQuery [L59]

