[web] GET /api/ui/offices/{id}/office-users  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetOfficeUsers)  [L139–L150] [auth=Authentication.UserPolicy]
  └─ maps_to OfficeUserWithUserDto [L142]
  └─ calls OfficeRepository.ReadQuery [L142]
  └─ queries Office [L142]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L142]

