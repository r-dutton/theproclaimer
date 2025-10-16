[web] GET /core/v1/users/profile  (Dataverse.Api.External.Controllers.v1.Core.UsersController.Profile)  [L54–L59] status=200
  └─ maps_to UserWithTenantIdDto [L57]
    └─ automapper.registration ExternalApiMappingProfile (User->UserWithTenantIdDto) [L66]
  └─ calls UserRepository.ReadQuery [L57]
  └─ query User [L57]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L57]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L58]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

