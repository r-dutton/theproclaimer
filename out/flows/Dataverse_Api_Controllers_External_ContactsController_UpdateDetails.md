[web] PUT /  (Dataverse.Api.Controllers.External.ContactsController.UpdateDetails)  [L62–L66] status=200
  └─ sends_request CreateOrUpdateContactCommand [L65]
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

