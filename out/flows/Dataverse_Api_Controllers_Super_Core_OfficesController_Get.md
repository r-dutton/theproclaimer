[web] GET /api/super/offices/{id:Guid}  (Dataverse.Api.Controllers.Super.Core.OfficesController.Get)  [L65–L70] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to OfficeDto [L68]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
  └─ calls OfficeRepository.ReadQuery [L68]
  └─ query Office [L68]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L68]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository
    └─ mappings 1
      └─ OfficeDto

