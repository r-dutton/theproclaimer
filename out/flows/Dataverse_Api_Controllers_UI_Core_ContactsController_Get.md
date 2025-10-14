[web] GET /api/ui/contacts/{id}  (Dataverse.Api.Controllers.UI.Core.ContactsController.Get)  [L83–L93] [auth=Authentication.UserPolicy]
  └─ maps_to ContactDto [L88]
    └─ automapper.registration DataverseMappingProfile (Contact->ContactDto) [L157]
  └─ calls ContactRepository.ReadQuery [L88]
  └─ queries Contact [L88]
    └─ reads_from DVS_Clients
  └─ uses_service IControlledRepository<Contact>
    └─ method ReadQuery [L88]
  └─ sends_request CanIAccessContactQuery [L86]
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

