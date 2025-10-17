[web] PUT /api/matters/move  (Workpapers.Next.API.Controllers.Workpapers.MattersController.Move)  [L218–L230] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request MoveMattersCommand -> MoveMattersCommandHandler [L227]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Matters.MoveMattersCommandHandler.Handle [L38–L168]
      └─ uses_service IControlledRepository<Matter> (Scoped (inferred))
        └─ method ReadQuery [L55]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatterRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ MoveMattersCommand
    └─ handlers 1
      └─ MoveMattersCommandHandler

