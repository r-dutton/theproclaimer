[web] POST /api/ui/trials/update-user/{referralId:guid}  (Dataverse.Api.Controllers.UI.Trial.TrialsAuthenticatedController.AddUserToReferral)  [L28–L33] status=201 [auth=Authentication.UserPolicy]
  └─ uses_service UserService
    └─ method GetUserId [L31]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
        └─ uses_service User
          └─ method GetUserId [L43]
            └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
            └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
        └─ uses_service Guid?
          └─ method GetUserId [L40]
            └─ ... (no implementation details available)
  └─ sends_request AddUserToReferralCommand -> AddUserToReferralCommandHandler [L32]
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.AddUserToReferralCommandHandler.Handle [L38–L67]
      └─ calls TenantRepository.WriteTable [L55]
      └─ calls UserRepository.ReadQuery [L53]
      └─ maps_to UserDto [L53]
        └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
        └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ AddUserToReferralCommand
    └─ handlers 1
      └─ AddUserToReferralCommandHandler
    └─ mappings 1
      └─ UserDto

