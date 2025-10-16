[web] POST /api/ui/integration-settings/  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Post)  [L61–L74] status=201 [auth=Authentication.AdminPolicy]
  └─ calls IntegrationSettingsRepository (methods: Add,WriteQuery) [L68]
  └─ insert IntegrationSettings [L68]
    └─ reads_from IntegrationSettingss
  └─ write IntegrationSettings [L64]
    └─ reads_from IntegrationSettingss
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ IntegrationSettings writes=2 reads=0

