[web] PUT /api/internal/users/  (Dataverse.Api.Controllers.Internal.Core.UsersController.UpdateDetails)  [L166–L176] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request UpdateUserCommand -> UpdateUserCommandHandler [L173]
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
    └─ requests 1
      └─ UpdateUserCommand
    └─ handlers 1
      └─ UpdateUserCommandHandler
    └─ mappings 1
      └─ UserWithIdentityInfoDto

