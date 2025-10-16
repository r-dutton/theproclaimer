[web] GET /api/ui/offices/{id}/office-users  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetOfficeUsers)  [L139–L150] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to OfficeUserWithUserDto [L142]
  └─ calls OfficeRepository.ReadQuery [L142]
  └─ query Office [L142]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L142]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository
    └─ mappings 1
      └─ OfficeUserWithUserDto

