[web] GET /api/ui/offices/code/{code}  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetByCode)  [L92–L100] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to OfficeDto [L95]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
  └─ calls OfficeRepository.ReadQuery [L95]
  └─ query Office [L95]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L95]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository
    └─ mappings 1
      └─ OfficeDto

