[web] PUT /api/ui/contacts/{id:guid}  (Dataverse.Api.Controllers.UI.Core.ContactsController.Update)  [L126–L142] [auth=Authentication.UserPolicy]
  └─ calls ContactRepository.WriteQuery [L132]
  └─ writes_to Contact [L132]
    └─ reads_from DVS_Clients
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L137]
  └─ uses_service IControlledRepository<Contact>
    └─ method WriteQuery [L132]
  └─ sends_request CanIAccessContactQuery [L136]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Contacts.CanIAccessContactQueryHandler.Handle [L36–L90]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L76]
      └─ uses_service IControlledRepository<Contact>
        └─ method ReadQuery [L74]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L65]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L69]
      └─ uses_service UserService
        └─ method GetUserId [L68]
      └─ uses_cache IDistributedCache [L86]
        └─ method SetRecordAsync [write] [L86]
      └─ uses_cache IDistributedCache [L72]
        └─ method DoesRecordExistAsync [access] [L72]
      └─ uses_cache IDistributedCache [L70]
        └─ method CreateAccessKey [write] [L70]

