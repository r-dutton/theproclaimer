[web] POST /api/ui/contacts/  (Dataverse.Api.Controllers.UI.Core.ContactsController.Create)  [L110–L124] status=201 [auth=Authentication.UserPolicy]
  └─ calls ContactRepository.Add [L121]
  └─ insert Contact [L121]
    └─ reads_from DVS_Clients
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L118]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Contact>
    └─ method Add [L121]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessOfficeQuery [L115]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessOfficeQueryHandler.Handle [L39–L110]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L66]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
      └─ uses_service IControlledRepository<OfficeUser>
        └─ method ReadQuery [L86]
          └─ ... (no implementation details available)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L60]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L65]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L103]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L69]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L66]

