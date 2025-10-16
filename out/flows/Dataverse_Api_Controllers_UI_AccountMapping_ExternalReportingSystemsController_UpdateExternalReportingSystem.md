[web] PUT /api/ui/account-mapping/external-reporting-systems/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.UpdateExternalReportingSystem)  [L73–L78] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request UpdateExternalReportingSystemCommand -> UpdateExternalReportingSystemCommandHandler [L77]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.ExternalReportingSystems.UpdateExternalReportingSystemCommandHandler.Handle [L49–L79]
      └─ maps_to ExternalReportingSystemDto [L76]
      └─ uses_service UserService
        └─ method IsMaster [L67]
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
      └─ uses_service IControlledRepository<ExternalReportingSystem> (Scoped (inferred))
        └─ method WriteQuery [L64]
          └─ implementation Dataverse.Data.Repository.AccountMapping.ExternalReportingSystemRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ UpdateExternalReportingSystemCommand
    └─ handlers 1
      └─ UpdateExternalReportingSystemCommandHandler
    └─ mappings 1
      └─ ExternalReportingSystemDto

