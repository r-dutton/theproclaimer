[web] POST /api/internal/messages/send-message  (Dataverse.Api.Controllers.Internal.MessagesController.SendMessage)  [L71–L77] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request SendMessageCommand -> SendMessageCommandHandler [L74]
    └─ handled_by Dataverse.ApplicationService.Commands.Messaging.SendMessageCommandHandler.Handle [L55–L167]
      └─ calls UserRepository.ReadQuery [L83]
      └─ maps_to UserUltraLightDto [L85]
      └─ uses_service MockMessagingService
        └─ method RequestNotificationMessage [L125]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Message> (Scoped (inferred))
        └─ method Add [L113]
          └─ implementation Dataverse.Data.Repository.Users.MessageRepository.Add
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L86]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service UserService
        └─ method GetUserAsync [L84]
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
    └─ requests 1
      └─ SendMessageCommand
    └─ handlers 1
      └─ SendMessageCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ UserUltraLightDto

