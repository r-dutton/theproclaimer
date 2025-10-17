[web] POST /api/ui/account-mapping/benchmark-code-sets  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.CreateBenchmarkCodeSet)  [L55–L62] status=201 [auth=Authentication.AdminPolicy]
  └─ sends_request CreateBenchmarkCodeSetCommand -> CreateBenchmarkCodeSetCommandHandler [L59]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodeSets.CreateBenchmarkCodeSetCommandHandler.Handle [L19–L39]
      └─ uses_service IControlledRepository<BenchmarkCodeSet> (Scoped (inferred))
        └─ method Add [L35]
          └─ implementation Dataverse.Data.Repository.AccountMapping.BenchmarkCodeSetRepository.Add
      └─ uses_service UserService
        └─ method IsMaster [L32]
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
      └─ CreateBenchmarkCodeSetCommand
    └─ handlers 1
      └─ CreateBenchmarkCodeSetCommandHandler

