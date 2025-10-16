[web] PUT /api/ui/users/{id}  (Dataverse.Api.Controllers.UI.Core.UsersController.Update)  [L242–L247] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service UserService
    └─ method GetUserId [L245]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
        └─ uses_service User
          └─ method GetUserId [L43]
            └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
            └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
        └─ uses_service Guid?
          └─ method GetUserId [L40]
            └─ ... (no implementation details available)
  └─ sends_request UpdateUserCommand -> UpdateUserCommandHandler [L246]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.UpdateUserCommandHandler.Handle [L42–L111]
      └─ calls TeamRepository.ReadQuery [L78]
      └─ calls OfficeRepository.ReadQuery [L77]
      └─ calls UserRepository.WriteQuery [L68]
      └─ maps_to UserWithIdentityInfoDto [L95]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L100]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
        └─ method ReadQuery [L71]
          └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.ReadQuery
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ UpdateUserCommand
    └─ handlers 1
      └─ UpdateUserCommandHandler
    └─ mappings 1
      └─ UserWithIdentityInfoDto

