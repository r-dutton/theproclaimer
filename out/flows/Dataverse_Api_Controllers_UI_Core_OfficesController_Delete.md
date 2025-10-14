[web] DELETE /api/ui/offices/{id:Guid}  (Dataverse.Api.Controllers.UI.Core.OfficesController.Delete)  [L234–L239] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls OfficeRepository.WriteQuery [L237]
  └─ writes_to Office [L237]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method WriteQuery [L237]

