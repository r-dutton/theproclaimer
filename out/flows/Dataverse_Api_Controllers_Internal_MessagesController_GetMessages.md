[web] GET /api/internal/messages/  (Dataverse.Api.Controllers.Internal.MessagesController.GetMessages)  [L32–L41] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls MessageRepository.ReadQuery [L35]
  └─ query Message [L35]
    └─ reads_from Messages
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Message writes=0 reads=1

