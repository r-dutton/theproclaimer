[web] GET /api/internal/contacts/{id}  (Dataverse.Api.Controllers.Internal.Core.ContactsController.Get)  [L46–L52] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to ContactDto [L49]
    └─ automapper.registration DataverseMappingProfile (Contact->ContactDto) [L158]
  └─ calls ContactRepository.ReadQuery [L49]
  └─ query Contact [L49]
    └─ reads_from DVS_Clients
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Contact writes=0 reads=1
    └─ mappings 1
      └─ ContactDto

