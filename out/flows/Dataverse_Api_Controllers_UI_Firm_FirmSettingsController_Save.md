[web] PUT /api/ui/firm/settings/  (Dataverse.Api.Controllers.UI.Firm.FirmSettingsController.Save)  [L49–L64] status=200 [auth=Authentication.AdminPolicy]
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

