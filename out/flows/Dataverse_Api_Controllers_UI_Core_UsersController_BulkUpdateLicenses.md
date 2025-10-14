[web] PUT /api/ui/users/bulk/licenses  (Dataverse.Api.Controllers.UI.Core.UsersController.BulkUpdateLicenses)  [L264–L275] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request UpdateUserCommand [L271]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.UpdateUserCommandHandler.Handle [L42–L111]
      └─ maps_to UserWithIdentityInfoDto [L95]
      └─ uses_service IControlledRepository<FirmSettings>
        └─ method ReadQuery [L71]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L77]
      └─ uses_service IControlledRepository<Team>
        └─ method ReadQuery [L78]
      └─ uses_service IControlledRepository<User>
        └─ method WriteQuery [L68]
      └─ uses_service IMapper
        └─ method Map [L95]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L100]

