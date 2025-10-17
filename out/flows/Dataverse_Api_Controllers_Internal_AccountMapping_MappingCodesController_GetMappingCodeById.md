[web] GET /api/internal/account-mapping/mapping-codes/{id:guid}  (Dataverse.Api.Controllers.Internal.AccountMapping.MappingCodesController.GetMappingCodeById)  [L32–L36] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetMappingCodeQuery -> GetMappingCodeQueryHandler [L35]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.MappingCodes.GetMappingCodeQueryHandler.Handle [L38–L75]
      └─ maps_to MappingCodeDto [L68]
      └─ uses_service IControlledRepository<ExcludedMappingCode> (Scoped (inferred))
        └─ method ReadQuery [L62]
          └─ implementation Dataverse.Data.Repository.AccountMapping.ExcludedMappingCodeRepository.ReadQuery
      └─ uses_service UserService
        └─ method IsMaster [L59]
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
      └─ uses_service IControlledRepository<MappingCode> (Scoped (inferred))
        └─ method ReadQuery [L56]
          └─ implementation Dataverse.Data.Repository.AccountMapping.MappingCodeRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetMappingCodeQuery
    └─ handlers 1
      └─ GetMappingCodeQueryHandler
    └─ mappings 1
      └─ MappingCodeDto

