[web] GET /api/super/offices/  (Dataverse.Api.Controllers.Super.Core.OfficesController.GetAll)  [L56–L63] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to OfficeLightDto [L59]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeLightDto) [L141]
  └─ calls OfficeRepository.ReadQuery [L59]
  └─ query Office [L59]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L59]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository
    └─ mappings 1
      └─ OfficeLightDto

