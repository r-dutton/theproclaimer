[web] GET /api/ui/contacts/{id}/children  (Dataverse.Api.Controllers.UI.Core.ContactsController.GetChildContacts)  [L95–L108] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ContactSearchDto [L98]
    └─ automapper.registration DataverseMappingProfile (Contact->ContactSearchDto) [L160]
  └─ calls ContactRepository.ReadQuery [L98]
  └─ query Contact [L98]
    └─ reads_from DVS_Clients
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L100]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Contact>
    └─ method ReadQuery [L98]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L100]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

