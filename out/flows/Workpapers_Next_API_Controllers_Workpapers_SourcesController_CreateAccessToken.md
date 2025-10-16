[web] POST /api/sources/{type}/create-access-token  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.CreateAccessToken)  [L179–L185] status=201 [auth=AuthorizationPolicies.User]
  └─ uses_service IConnectionApiService (AddSingleton)
    └─ method GetApiMethods [L182]
      └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]
  └─ uses_service UserService
    └─ method GetUserId [L182]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

