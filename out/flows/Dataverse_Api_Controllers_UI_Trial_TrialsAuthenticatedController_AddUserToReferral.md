[web] POST /api/ui/trials/update-user/{referralId:guid}  (Dataverse.Api.Controllers.UI.Trial.TrialsAuthenticatedController.AddUserToReferral)  [L28–L33] [auth=Authentication.UserPolicy]
  └─ uses_service UserService
    └─ method GetUserId [L31]
  └─ sends_request AddUserToReferralCommand [L32]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.AddUserToReferralCommandHandler.Handle [L38–L67]
      └─ calls TenantRepository.WriteTable [L55]
      └─ maps_to UserDto [L53]
        └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
        └─ automapper.registration CirrusMappingProfile (User->UserDto) [L110]
        └─ automapper.registration WorkpapersMappingProfile (User->UserDto) [L106]
        └─ automapper.registration DataverseMappingProfile (User->UserDto) [L76]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L53]

