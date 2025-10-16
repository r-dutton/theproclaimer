[web] GET /api/internal/offices/{id}  (Dataverse.Api.Controllers.Internal.Core.OfficesController.Get)  [L47–L53] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to OfficeDto [L50]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
  └─ calls OfficeRepository.ReadQuery [L50]
  └─ query Office [L50]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L50]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository
    └─ mappings 1
      └─ OfficeDto

