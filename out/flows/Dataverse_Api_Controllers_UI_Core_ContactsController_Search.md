[web] GET /api/ui/contacts/  (Dataverse.Api.Controllers.UI.Core.ContactsController.Search)  [L55–L81] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindContactsQuery [L69]
    └─ handled_by Dataverse.ApplicationService.Queries.Contacts.FindContactsQueryHandler.Handle [L62–L131]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L85]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserId [L85]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service IControlledRepository<Contact>
        └─ method ReadQuery [L83]
          └─ ... (no implementation details available)

