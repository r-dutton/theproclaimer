[web] POST /api/dataverse/users/core  (Workpapers.Next.API.Controllers.DataverseController.UpdateCoreUserChanges)  [L77–L87] [auth=AuthorizationPolicies.M2M]
  └─ sends_request ProcessCoreUserChangesFromDataverseCommand [L81]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Dataverse.ProcessCoreUserChangesFromDataverseCommandHandler.Handle [L28–L73]
      └─ uses_service UnitOfWork
        └─ method Table [L39]

