[web] PUT /api/super/firm/settings/  (Dataverse.Api.Controllers.Super.TenantFirmSettingsController.Save)  [L49–L64] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls FirmSettingsRepository (methods: Add,WriteQuery) [L57]
  └─ insert FirmSettings [L57]
    └─ reads_from FirmSettingss
  └─ write FirmSettings [L52]
    └─ reads_from FirmSettingss
  └─ sends_request UpdateUsersSupportTicketCreationPermissionCommand -> UpdateUsersSupportTicketCreationPermissionCommandHandler [L62]
    └─ handled_by Dataverse.ApplicationService.Commands.Firms.UpdateUsersSupportTicketCreationPermissionCommandHandler.Handle [L26–L69]
      └─ calls UserRepository.WriteQuery [L47]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ FirmSettings writes=2 reads=0
    └─ requests 1
      └─ UpdateUsersSupportTicketCreationPermissionCommand
    └─ handlers 1
      └─ UpdateUsersSupportTicketCreationPermissionCommandHandler

