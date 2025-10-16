[web] GET /api/ui/offices/{id}/office-users  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetOfficeUsers)  [L139–L150] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to OfficeUserWithUserDto [L142]
  └─ calls OfficeRepository.ReadQuery [L142]
  └─ query Office [L142]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L142]
      └─ ... (no implementation details available)

