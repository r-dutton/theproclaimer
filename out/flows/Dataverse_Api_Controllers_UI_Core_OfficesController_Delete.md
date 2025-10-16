[web] DELETE /api/ui/offices/{id:Guid}  (Dataverse.Api.Controllers.UI.Core.OfficesController.Delete)  [L234–L239] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls OfficeRepository.WriteQuery [L237]
  └─ write Office [L237]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method WriteQuery [L237]
      └─ ... (no implementation details available)

