[web] PUT /api/super/firm/settings/  (Dataverse.Api.Controllers.Super.TenantFirmSettingsController.Save)  [L49–L64] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls FirmSettingsRepository.Add [L57]
  └─ calls FirmSettingsRepository.WriteQuery [L52]
  └─ insert FirmSettings [L57]
    └─ reads_from FirmSettingss
  └─ write FirmSettings [L52]
    └─ reads_from FirmSettingss
  └─ uses_service IControlledRepository<FirmSettings>
    └─ method WriteQuery [L52]
      └─ ... (no implementation details available)
  └─ sends_request UpdateUsersSupportTicketCreationPermissionCommand [L62]
    └─ handled_by Dataverse.ApplicationService.Commands.Firms.UpdateUsersSupportTicketCreationPermissionCommandHandler.Handle [L26–L69]
      └─ uses_service IControlledRepository<User>
        └─ method WriteQuery [L47]
          └─ ... (no implementation details available)

