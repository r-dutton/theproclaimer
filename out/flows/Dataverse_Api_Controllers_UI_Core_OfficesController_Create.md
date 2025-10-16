[web] POST /api/ui/offices/  (Dataverse.Api.Controllers.UI.Core.OfficesController.Create)  [L212–L219] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls OfficeRepository.Add [L217]
  └─ insert Office [L217]
    └─ reads_from Offices
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L215]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Office>
    └─ method Add [L217]
      └─ ... (no implementation details available)

