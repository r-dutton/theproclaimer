[web] GET /api/ui/contacts/{id}  (Dataverse.Api.Controllers.UI.Core.ContactsController.Get)  [L83–L93] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ContactDto [L88]
    └─ automapper.registration DataverseMappingProfile (Contact->ContactDto) [L158]
  └─ calls ContactRepository.ReadQuery [L88]
  └─ query Contact [L88]
    └─ reads_from DVS_Clients
  └─ uses_service IControlledRepository<Contact>
    └─ method ReadQuery [L88]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessContactQuery [L86]
    └─ handled_by Dataverse.ApplicationService.Queries.Contacts.CanIAccessContactQueryHandler.Handle [L36–L90]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L76]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserId [L68]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L69]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L65]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service IControlledRepository<Contact>
        └─ method ReadQuery [L74]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L86]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L72]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L70]

