[web] PUT /api/ui/tenants/update-license  (Dataverse.Api.Controllers.UI.TenantController.UpdateLicense)  [L120–L125] [auth=Authentication.OwnerPolicy]
  └─ sends_request UserUpdateLicenseCommand [L123]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.UserUpdateLicenseCommandHandler.Handle [L29–L124]
      └─ uses_service EmailSender
        └─ method SendEmailFromOrdersAsync [L115]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L93]
      └─ uses_service SmtpSettings
        └─ method FromOrdersAU [L80]
      └─ uses_service TenantLicenseService
        └─ method GetFirmSubscriptions [L61]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L59]
      └─ uses_service UserService
        └─ method GetUserIfPresentAsync [L60]

