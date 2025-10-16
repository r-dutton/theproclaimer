[web] GET /api/internal/users/intercom-profile  (Dataverse.Api.Controllers.Internal.Core.UsersController.IntercomProfile)  [L111–L139] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IntercomService
    └─ method GetContactByExternalId [L119]
      └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.GetContactByExternalId [L17-L124]
        └─ uses_client IntercomClient [L31]
        └─ uses_service IntercomClient
          └─ method GetContacts [L31]
            └─ implementation Dataverse.Services.ModuleIntegrations.Clients.IntercomClient.GetContacts [L25-L174]
  └─ uses_service UserService
    └─ method GetUserAsync [L114]
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
  └─ impact_summary
    └─ clients 1
      └─ IntercomClient
    └─ services 2
      └─ IntercomService
      └─ UserService

