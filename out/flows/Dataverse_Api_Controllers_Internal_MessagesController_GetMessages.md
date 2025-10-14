[web] GET /api/internal/messages/  (Dataverse.Api.Controllers.Internal.MessagesController.GetMessages)  [L32–L41] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls MessageRepository.ReadQuery [L35]
  └─ queries Message [L35]
    └─ reads_from Messages
  └─ uses_service IControlledRepository<Message>
    └─ method ReadQuery [L35]

