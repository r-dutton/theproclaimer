[web] POST /api/ui/account-mapping/mapping-codes/for-client-group/{clientGroupId:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.CreateMappingCode)  [L92–L100] status=201 [auth=Authentication.UserPolicy,Authentication.UserPolicy]
  └─ sends_request CreateMappingCodeCommand -> CreateMappingCodeCommandHandler [L97]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.MappingCodes.CreateMappingCodeCommandHandler.Handle [L41–L75]
      └─ maps_to MappingCodeDto [L73]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L69]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<MappingCode> (Scoped (inferred))
        └─ method ReadQuery [L60]
          └─ implementation Dataverse.Data.Repository.AccountMapping.MappingCodeRepository.ReadQuery
      └─ uses_service UserService
        └─ method IsMaster [L58]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
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
      └─ CreateMappingCodeCommand
    └─ handlers 1
      └─ CreateMappingCodeCommandHandler
    └─ mappings 1
      └─ MappingCodeDto

