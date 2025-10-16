[web] PUT /api/ui/tenants/update-license  (Dataverse.Api.Controllers.UI.TenantController.UpdateLicense)  [L120–L125] status=200 [auth=Authentication.OwnerPolicy]
  └─ sends_request UserUpdateLicenseCommand [L123]
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.UserUpdateLicenseCommandHandler.Handle [L29–L124]
      └─ uses_service UserService
        └─ method GetUserIfPresentAsync [L60]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserIfPresentAsync [L15-L185]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L59]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service TenantLicenseService
        └─ method GetFirmSubscriptions [L61]
          └─ implementation Dataverse.ApplicationService.Services.TenantLicenseService.GetFirmSubscriptions [L22-L185]
      └─ uses_service EmailSender
        └─ method SendEmailFromOrdersAsync [L115]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L93]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service SmtpSettings
        └─ method FromOrdersAU [L80]
          └─ ... (no implementation details available)

