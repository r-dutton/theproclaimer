[web] PUT /api/ui/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.UpdateBenchmarkCodeSet)  [L70–L75] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request UpdateBenchmarkCodeSetCommand -> UpdateBenchmarkCodeSetCommandHandler [L74]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodeSets.UpdateBenchmarkCodeSetCommandHandler.Handle [L27–L55]
      └─ uses_service IControlledRepository<BenchmarkCodeSet> (Scoped (inferred))
        └─ method WriteQuery [L41]
          └─ implementation Dataverse.Data.Repository.AccountMapping.BenchmarkCodeSetRepository.WriteQuery
      └─ uses_service UserService
        └─ method IsMaster [L40]
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
      └─ UpdateBenchmarkCodeSetCommand
    └─ handlers 1
      └─ UpdateBenchmarkCodeSetCommandHandler

