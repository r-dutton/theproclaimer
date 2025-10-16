[web] GET /core/v1/offices/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.Get)  [L52–L57] status=200
  └─ maps_to OfficeDto [L55]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
  └─ calls OfficeRepository.ReadQuery [L55]
  └─ query Office [L55]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L55]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository
    └─ mappings 1
      └─ OfficeDto

