[web] DELETE /api/super/firm/settings/  (Dataverse.Api.Controllers.Super.TenantFirmSettingsController.Delete)  [L69–L74] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls FirmSettingsRepository.Remove [L73]
  └─ calls FirmSettingsRepository.WriteQuery [L72]
  └─ writes_to FirmSettings [L73]
    └─ reads_from FirmSettingss
  └─ writes_to FirmSettings [L72]
    └─ reads_from FirmSettingss
  └─ uses_service IControlledRepository<FirmSettings>
    └─ method WriteQuery [L72]

