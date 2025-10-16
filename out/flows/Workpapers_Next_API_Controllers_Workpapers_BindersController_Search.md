[web] GET /api/binders/  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Search)  [L109–L114] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request FindBindersQuery [L113]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.FindBindersQueryHandler.Handle [L48–L167]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L89]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
      └─ uses_service UserService
        └─ method GetUser [L89]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L90]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method ReadQuery [L104]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L81]
          └─ ... (no implementation details available)

