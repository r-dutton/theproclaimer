[web] GET /api/ui/offices/{id}  (Dataverse.Api.Controllers.UI.Core.OfficesController.Get)  [L77–L83] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to OfficeDto [L80]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
  └─ calls OfficeRepository.ReadQuery [L80]
  └─ query Office [L80]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L80]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository
    └─ mappings 1
      └─ OfficeDto

