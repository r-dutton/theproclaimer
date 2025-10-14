[web] PUT /api/dataverse/synchronise-binder-status-as-user  (Workpapers.Next.API.Controllers.DataverseController.UpdateBinderStatusesAsUser)  [L338–L344] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId,AuthorizationPolicies.RequireDataverseTenantAndUserId]
  └─ sends_request SynchroniseBinderStatusCommand [L343]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Dataverse.SynchroniseBinderStatusCommandHandler.Handle [L28–L85]
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method ReadQuery [L49]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L78]
      └─ uses_service UnitOfWork
        └─ method Table [L71]

