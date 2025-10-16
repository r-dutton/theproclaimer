[web] PUT /api/internal/messages/{messageId:guid}/bulk-mark-as-sent  (Dataverse.Api.Controllers.Internal.MessagesController.BulkMarkAsSent)  [L56–L69] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls MessageRepository.WriteQuery [L59]
  └─ write Message [L59]
    └─ reads_from Messages
  └─ uses_service IControlledRepository<Message>
    └─ method WriteQuery [L59]
      └─ ... (no implementation details available)

