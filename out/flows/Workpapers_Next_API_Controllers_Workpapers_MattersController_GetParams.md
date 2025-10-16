[web] GET /api/matters  (Workpapers.Next.API.Controllers.Workpapers.MattersController.GetParams)  [L232–L240] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service IControlledRepository<MatterStatus> (AddScoped)
    └─ method ReadQuery [L235]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L239]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

