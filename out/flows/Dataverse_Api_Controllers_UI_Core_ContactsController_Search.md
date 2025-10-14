[web] GET /api/ui/contacts/  (Dataverse.Api.Controllers.UI.Core.ContactsController.Search)  [L55–L81] [auth=Authentication.UserPolicy]
  └─ sends_request FindContactsQuery [L69]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Contacts.FindContactsQueryHandler.Handle [L62–L131]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L85]
      └─ uses_service IControlledRepository<Contact>
        └─ method ReadQuery [L83]
      └─ uses_service UserService
        └─ method GetUserId [L85]

