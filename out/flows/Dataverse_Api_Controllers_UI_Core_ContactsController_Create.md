[web] POST /api/ui/contacts/  (Dataverse.Api.Controllers.UI.Core.ContactsController.Create)  [L110–L124] [auth=Authentication.UserPolicy]
  └─ calls ContactRepository.Add [L121]
  └─ writes_to Contact [L121]
    └─ reads_from DVS_Clients
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L118]
  └─ uses_service IControlledRepository<Contact>
    └─ method Add [L121]
  └─ sends_request CanIAccessOfficeQuery [L115]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessOfficeQueryHandler.Handle [L39–L110]
      └─ uses_service IControlledRepository<OfficeUser>
        └─ method ReadQuery [L86]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L60]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L65]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L66]
      └─ uses_cache IDistributedCache [L103]
        └─ method SetRecordAsync [write] [L103]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L69]
        └─ method DoesRecordExistAsync [access] [L69]
      └─ uses_cache IDistributedCache [L66]
        └─ method CreateAccessKey [write] [L66]

