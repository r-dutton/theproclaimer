[web] GET /api/internal/contacts/audit  (Dataverse.Api.Controllers.Internal.Core.ContactsController.GetAll)  [L40–L44] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls ContactRepository.ReadQuery [L43]
  └─ query Contact [L43]
    └─ reads_from DVS_Clients
  └─ uses_service IControlledRepository<Contact>
    └─ method ReadQuery [L43]
      └─ ... (no implementation details available)

