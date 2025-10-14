[web] GET /api/ui/contacts/{id}/children  (Dataverse.Api.Controllers.UI.Core.ContactsController.GetChildContacts)  [L95–L108] [auth=Authentication.UserPolicy]
  └─ maps_to ContactSearchDto [L98]
    └─ automapper.registration DataverseMappingProfile (Contact->ContactSearchDto) [L159]
  └─ calls ContactRepository.ReadQuery [L98]
  └─ queries Contact [L98]
    └─ reads_from DVS_Clients
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L100]
  └─ uses_service IControlledRepository<Contact>
    └─ method ReadQuery [L98]
  └─ uses_service UserService
    └─ method GetUserId [L100]

