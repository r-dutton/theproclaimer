[web] POST /api/internal/users/  (Dataverse.Api.Controllers.Internal.Core.UsersController.Create)  [L155–L161] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateUserCommand -> CreateOrLinkWithDataverseCommandHandler [L159]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.CreateOrLinkWithDataverseCommandHandler.Handle [L57–L149]
      └─ calls UserRepository (methods: Add,WriteQuery) [L136]
      └─ calls TeamRepository.ReadQuery [L87]
      └─ calls OfficeRepository.ReadQuery [L86]
      └─ uses_service EventPublisher (InstancePerLifetimeScope)
        └─ method PublishAllEventsForEntity [L145]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L141]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
        └─ method ReadQuery [L111]
          └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.ReadQuery
      └─ uses_service UserService
        └─ method IsInRole [L103]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsInRole [L15-L185]
            └─ calls UserRepository.ReadQuery [L133]
            └─ uses_service RequestInfoService
              └─ method GetIdentityId [L160]
                └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                      └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L82]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                      └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L71]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L81]
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ CreateOrUpdateUserCommand
    └─ handlers 1
      └─ CreateOrLinkWithDataverseCommandHandler

