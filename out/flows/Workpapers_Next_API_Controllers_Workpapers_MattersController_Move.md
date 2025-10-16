[web] PUT /api/matters/move  (Workpapers.Next.API.Controllers.Workpapers.MattersController.Move)  [L218–L230] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request MoveMattersCommand [L227]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Matters.MoveMattersCommandHandler.Handle [L38–L168]
      └─ uses_service IControlledRepository<Matter>
        └─ method ReadQuery [L55]
          └─ ... (no implementation details available)

