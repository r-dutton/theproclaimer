[web] PUT /api/ui/offices/{id}  (Dataverse.Api.Controllers.UI.Core.OfficesController.Update)  [L224–L229] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request UpdateOfficeWithUsersCommand [L227]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Offices.UpdateOfficeWithUsersCommandHandler.Handle [L30–L127]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L51]
      └─ uses_service IControlledRepository<Office>
        └─ method WriteQuery [L52]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L62]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]

