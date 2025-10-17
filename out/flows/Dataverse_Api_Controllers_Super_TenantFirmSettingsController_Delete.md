[web] DELETE /api/super/firm/settings/  (Dataverse.Api.Controllers.Super.TenantFirmSettingsController.Delete)  [L69–L74] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls FirmSettingsRepository (methods: Remove,WriteQuery) [L73]
  └─ delete FirmSettings [L73]
    └─ reads_from FirmSettingss
  └─ write FirmSettings [L72]
    └─ reads_from FirmSettingss
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ FirmSettings writes=2 reads=0

