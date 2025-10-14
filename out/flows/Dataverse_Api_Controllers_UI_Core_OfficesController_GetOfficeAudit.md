[web] GET /api/ui/offices/{id:Guid}/audit  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetOfficeAudit)  [L181–L206] [auth=Authentication.UserPolicy]
  └─ calls OfficeRepository.ReadQuery [L184]
  └─ queries Office [L184]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L184]

