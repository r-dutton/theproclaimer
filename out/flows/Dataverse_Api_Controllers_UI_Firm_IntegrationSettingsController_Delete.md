[web] DELETE /api/ui/integration-settings/{id:guid}  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Delete)  [L89–L94] status=200 [auth=Authentication.AdminPolicy]
  └─ calls IntegrationSettingsRepository (methods: Remove,WriteQuery) [L93]
  └─ delete IntegrationSettings [L93]
    └─ reads_from IntegrationSettingss
  └─ write IntegrationSettings [L92]
    └─ reads_from IntegrationSettingss
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ IntegrationSettings writes=2 reads=0

