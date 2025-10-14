[web] PUT /  (Dataverse.Api.Controllers.External.ContactsController.UpdateDetails)  [L62–L66]
  └─ sends_request CreateOrUpdateContactCommand [L65]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Clients.CreateOrUpdateContactCommandHandler.Handle [L39–L79]
      └─ maps_to ContactDto [L77]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L54]
      └─ uses_service IControlledRepository<Contact>
        └─ method WriteQuery [L61]
      └─ uses_service IMapper
        └─ method Map [L77]

