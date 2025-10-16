[web] GET /api/binders/client-queries  (Workpapers.Next.API.Controllers.Workpapers.BindersController.SearchClientQuery)  [L116–L121] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request FindClientQueriesForBindersQuery [L120]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.FindClientQueryHandler.Handle [L40–L119]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L85]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
      └─ uses_service UserService
        └─ method GetUser [L85]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L84]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L70]
          └─ ... (no implementation details available)

