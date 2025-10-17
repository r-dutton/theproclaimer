[web] GET /api/internal/account-mapping/external-reporting-systems/{id:guid}/mapping-codes  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetMappingCodes)  [L57–L61] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetExternalReportingSystemMappingCodesQuery -> GetExternalReportingSystemMappingCodesQueryHandler [L60]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodesQueryHandler.Handle [L33–L72]
      └─ maps_to ExternalReportingSystemMappingCodeDto [L54]
        └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L243]
      └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode> (Scoped (inferred))
        └─ method ReadQuery [L54]
          └─ implementation Dataverse.Data.Repository.AccountMapping.ExternalReportingSystemMappingCodeRepository.ReadQuery
      └─ uses_service UserService
        └─ method IsMaster [L53]
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
      └─ GetExternalReportingSystemMappingCodesQuery
    └─ handlers 1
      └─ GetExternalReportingSystemMappingCodesQueryHandler
    └─ mappings 1
      └─ ExternalReportingSystemMappingCodeDto

