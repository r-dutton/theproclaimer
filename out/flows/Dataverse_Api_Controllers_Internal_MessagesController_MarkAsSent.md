[web] PUT /api/internal/messages/{messageId:guid}/mark-as-sent  (Dataverse.Api.Controllers.Internal.MessagesController.MarkAsSent)  [L46–L54] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls MessageRepository.WriteQuery [L49]
  └─ write Message [L49]
    └─ reads_from Messages
  └─ uses_service IControlledRepository<Message>
    └─ method WriteQuery [L49]
      └─ ... (no implementation details available)

