[web] GET /core/v1/users/profile  (Dataverse.Api.External.Controllers.v1.Core.UsersController.Profile)  [L54–L59] status=200
  └─ maps_to UserWithTenantIdDto [L57]
    └─ automapper.registration ExternalApiMappingProfile (User->UserWithTenantIdDto) [L66]
  └─ calls UserRepository.ReadQuery [L57]
  └─ query User [L57]
  └─ uses_service UserService
    └─ method GetUserId [L58]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
        └─ uses_service User
          └─ method GetUserId [L43]
            └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
            └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
        └─ uses_service Guid?
          └─ method GetUserId [L40]
            └─ ... (no implementation details available)
  └─ uses_service UserRepository
    └─ method ReadQuery [L57]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 2
      └─ UserRepository
      └─ UserService
    └─ mappings 1
      └─ UserWithTenantIdDto

