[web] GET /api/sources/{type}/authorization-url  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetAuthorizationUrl)  [L171–L177] [auth=AuthorizationPolicies.User]
  └─ uses_service IConnectionApiService (AddSingleton)
    └─ method GetApiMethods [L174]
  └─ uses_service UserService
    └─ method GetUserId [L174]

