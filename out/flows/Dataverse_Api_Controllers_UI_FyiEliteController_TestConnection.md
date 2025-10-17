[web] GET /api/ui/fyi-elite/{id}/test-connection  (Dataverse.Api.Controllers.UI.FyiEliteController.TestConnection)  [L64–L88] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L69]
  └─ write SyncConfiguration [L69]
    └─ reads_from SyncConfigurations
  └─ uses_service UserService
    └─ method GetUserAsync [L71]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
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
  └─ uses_service IDatagetFyiEliteService (AddTransient)
    └─ method TestConnection [L68]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiEliteService.TestConnection [L13-L53]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0
    └─ services 2
      └─ IDatagetFyiEliteService
      └─ UserService

