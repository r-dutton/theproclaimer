[web] GET /api/super/firm/settings/  (Dataverse.Api.Controllers.Super.TenantFirmSettingsController.Get)  [L38–L44] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to FirmSettingsDto [L41]
    └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L121]
  └─ calls FirmSettingsRepository.ReadQuery [L41]
  └─ query FirmSettings [L41]
    └─ reads_from FirmSettingss
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ FirmSettings writes=0 reads=1
    └─ mappings 1
      └─ FirmSettingsDto

