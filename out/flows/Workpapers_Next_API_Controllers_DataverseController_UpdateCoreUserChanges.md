[web] POST /api/dataverse/users/core  (Workpapers.Next.API.Controllers.DataverseController.UpdateCoreUserChanges)  [L77–L87] status=201 [auth=AuthorizationPolicies.M2M]
  └─ sends_request ProcessCoreUserChangesFromDataverseCommand -> ProcessCoreUserChangesFromDataverseCommandHandler [L81]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Dataverse.ProcessCoreUserChangesFromDataverseCommandHandler.Handle [L28–L73]
      └─ uses_service UnitOfWork
        └─ method Table [L39]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ requests 1
      └─ ProcessCoreUserChangesFromDataverseCommand
    └─ handlers 1
      └─ ProcessCoreUserChangesFromDataverseCommandHandler

