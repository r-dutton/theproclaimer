[web] GET /api/ui/offices/{id}/find-office-users  (Dataverse.Api.Controllers.UI.Core.OfficesController.FindOfficeUsers)  [L109–L130] [auth=Authentication.UserPolicy]
  └─ calls OfficeRepository.ReadQuery [L116]
  └─ queries Office [L116]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L116]

