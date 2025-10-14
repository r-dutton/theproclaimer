[web] POST /api/sources/{type}/create-access-token  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.CreateAccessToken)  [L179–L185] [auth=AuthorizationPolicies.User]
  └─ uses_service IConnectionApiService (AddSingleton)
    └─ method GetApiMethods [L182]
  └─ uses_service UserService
    └─ method GetUserId [L182]

