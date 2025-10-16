[web] GET /api/internal/contacts/{id}  (Dataverse.Api.Controllers.Internal.Core.ContactsController.Get)  [L46–L52] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to ContactDto [L49]
    └─ automapper.registration DataverseMappingProfile (Contact->ContactDto) [L158]
  └─ calls ContactRepository.ReadQuery [L49]
  └─ query Contact [L49]
    └─ reads_from DVS_Clients
  └─ uses_service IControlledRepository<Contact>
    └─ method ReadQuery [L49]
      └─ ... (no implementation details available)

