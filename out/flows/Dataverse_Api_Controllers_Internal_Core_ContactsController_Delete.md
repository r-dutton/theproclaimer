[web] DELETE /api/internal/contacts/{id}  (Dataverse.Api.Controllers.Internal.Core.ContactsController.Delete)  [L71–L79] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls ContactRepository.Remove [L78]
  └─ calls ContactRepository.WriteQuery [L74]
  └─ delete Contact [L78]
    └─ reads_from DVS_Clients
  └─ write Contact [L74]
    └─ reads_from DVS_Clients
  └─ uses_service IControlledRepository<Contact>
    └─ method WriteQuery [L74]
      └─ ... (no implementation details available)

