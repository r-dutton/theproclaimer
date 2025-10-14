[web] POST /api/ui/offices/  (Dataverse.Api.Controllers.UI.Core.OfficesController.Create)  [L212–L219] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls OfficeRepository.Add [L217]
  └─ writes_to Office [L217]
    └─ reads_from Offices
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L215]
  └─ uses_service IControlledRepository<Office>
    └─ method Add [L217]

