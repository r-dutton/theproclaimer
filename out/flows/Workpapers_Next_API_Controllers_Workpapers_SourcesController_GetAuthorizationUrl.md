[web] GET /api/sources/{type}/authorization-url  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetAuthorizationUrl)  [L171–L177] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service IConnectionApiService (AddSingleton)
    └─ method GetApiMethods [L174]
      └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]
  └─ uses_service UserService
    └─ method GetUserId [L174]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

