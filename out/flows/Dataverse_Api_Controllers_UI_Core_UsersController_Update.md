[web] PUT /api/ui/users/{id}  (Dataverse.Api.Controllers.UI.Core.UsersController.Update)  [L242–L247] [auth=Authentication.UserPolicy]
  └─ uses_service UserService
    └─ method GetUserId [L245]
  └─ sends_request UpdateUserCommand [L246]
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

