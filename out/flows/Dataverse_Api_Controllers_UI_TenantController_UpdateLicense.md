[web] PUT /api/ui/tenants/update-license  (Dataverse.Api.Controllers.UI.TenantController.UpdateLicense)  [L120–L125] status=200 [auth=Authentication.OwnerPolicy]
  └─ sends_request UserUpdateLicenseCommand -> UserUpdateLicenseCommandHandler [L123]
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.UserUpdateLicenseCommandHandler.Handle [L29–L124]
      └─ uses_service EmailSender
        └─ method SendEmailFromOrdersAsync [L115]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L93]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service SmtpSettings
        └─ method FromOrdersAU [L80]
          └─ ... (no implementation details available)
      └─ uses_service TenantLicenseService
        └─ method GetFirmSubscriptions [L61]
          └─ implementation Dataverse.ApplicationService.Services.TenantLicenseService.GetFirmSubscriptions [L22-L185]
            └─ uses_service TenantService
              └─ method GetFirmSubscriptions [L74]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetFirmSubscriptions [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service UserService
        └─ method GetUserIfPresentAsync [L60]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserIfPresentAsync [L15-L185]
            └─ calls UserRepository.ReadQuery [L133]
            └─ uses_service RequestInfoService
              └─ method GetIdentityId [L160]
                └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                      └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L82]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                      └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L71]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L81]
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L59]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
  └─ impact_summary
    └─ requests 1
      └─ UserUpdateLicenseCommand
    └─ handlers 1
      └─ UserUpdateLicenseCommandHandler
    └─ caches 1
      └─ IMemoryCache

