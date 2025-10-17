[web] PUT /api/ui/integration-settings/{id:guid}  (Dataverse.Api.Controllers.UI.Firm.IntegrationSettingsController.Put)  [L79–L84] status=200 [auth=Authentication.AdminPolicy]
  └─ calls IntegrationSettingsRepository.WriteQuery [L82]
  └─ write IntegrationSettings [L82]
    └─ reads_from IntegrationSettingss
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ IntegrationSettings writes=1 reads=0

