[web] DELETE /api/ui/firm/settings/  (Dataverse.Api.Controllers.UI.Firm.FirmSettingsController.Delete)  [L69–L74] [auth=Authentication.AdminPolicy]
  └─ calls FirmSettingsRepository.Remove [L73]
  └─ calls FirmSettingsRepository.WriteQuery [L72]
  └─ writes_to FirmSettings [L73]
    └─ reads_from FirmSettingss
  └─ writes_to FirmSettings [L72]
    └─ reads_from FirmSettingss
  └─ uses_service IControlledRepository<FirmSettings>
    └─ method WriteQuery [L72]

