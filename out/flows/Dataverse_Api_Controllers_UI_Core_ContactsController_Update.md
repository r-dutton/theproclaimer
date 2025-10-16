[web] PUT /api/ui/contacts/{id:guid}  (Dataverse.Api.Controllers.UI.Core.ContactsController.Update)  [L126–L142] status=200 [auth=Authentication.UserPolicy]
  └─ calls ContactRepository.WriteQuery [L132]
  └─ write Contact [L132]
    └─ reads_from DVS_Clients
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L137]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Contact>
    └─ method WriteQuery [L132]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessContactQuery [L136]
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

