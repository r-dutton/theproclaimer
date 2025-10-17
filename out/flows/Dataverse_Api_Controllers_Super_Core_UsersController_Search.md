[web] GET /api/super/users/all  (Dataverse.Api.Controllers.Super.Core.UsersController.Search)  [L78–L89] status=200 [auth=Authentication.MasterPolicy]
  └─ sends_request FindCentralUsersQuery -> FindCentralUsersQueryHandler [L88]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.FindCentralUsersQueryHandler.Handle [L48–L116]
      └─ calls TenantRepository.ReadTable [L69]
      └─ uses_service UserService
        └─ method GetIdentityId [L73]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetIdentityId [L15-L185]
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
  └─ impact_summary
    └─ requests 1
      └─ FindCentralUsersQuery
    └─ handlers 1
      └─ FindCentralUsersQueryHandler

