[web] POST /create-or-update  (Dataverse.Api.Controllers.External.ContactsController.CreateOrUpdate)  [L53–L60] status=201
  └─ sends_request CreateOrUpdateContactCommand [L56]
    └─ handled_by Dataverse.ApplicationService.Commands.Clients.CreateOrUpdateContactCommandHandler.Handle [L39–L79]
      └─ maps_to ContactDto [L77]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L54]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service IControlledRepository<Contact>
        └─ method WriteQuery [L61]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L77]
          └─ ... (no implementation details available)

