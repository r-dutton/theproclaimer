[web] PUT /api/super/firm/settings/  (Dataverse.Api.Controllers.Super.TenantFirmSettingsController.Save)  [L49–L64] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls FirmSettingsRepository.Add [L57]
  └─ calls FirmSettingsRepository.WriteQuery [L52]
  └─ writes_to FirmSettings [L57]
    └─ reads_from FirmSettingss
  └─ writes_to FirmSettings [L52]
    └─ reads_from FirmSettingss
  └─ uses_service IControlledRepository<FirmSettings>
    └─ method WriteQuery [L52]
  └─ sends_request UpdateUsersSupportTicketCreationPermissionCommand [L62]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Firms.UpdateUsersSupportTicketCreationPermissionCommandHandler.Handle [L26–L69]
      └─ uses_service IControlledRepository<User>
        └─ method WriteQuery [L47]

