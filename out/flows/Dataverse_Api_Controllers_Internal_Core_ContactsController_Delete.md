[web] DELETE /api/internal/contacts/{id}  (Dataverse.Api.Controllers.Internal.Core.ContactsController.Delete)  [L71–L79] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls ContactRepository.Remove [L78]
  └─ calls ContactRepository.WriteQuery [L74]
  └─ writes_to Contact [L78]
    └─ reads_from DVS_Clients
  └─ writes_to Contact [L74]
    └─ reads_from DVS_Clients
  └─ uses_service IControlledRepository<Contact>
    └─ method WriteQuery [L74]

