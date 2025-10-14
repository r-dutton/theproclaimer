[web] GET /api/matters  (Workpapers.Next.API.Controllers.Workpapers.MattersController.GetParams)  [L232–L240] [auth=AuthorizationPolicies.User]
  └─ uses_service IControlledRepository<MatterStatus> (AddScoped)
    └─ method ReadQuery [L235]
  └─ uses_service UserService
    └─ method GetUserId [L239]

