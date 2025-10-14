[web] PUT /api/dataverse/custom-statuses  (Workpapers.Next.API.Controllers.DataverseController.UpdateCustomStatusChanges)  [L126–L132] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L130]
  └─ sends_request ProcessDataverseCustomStatusCommand [L131]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Commands.Dataverse.ProcessDataverseCustomStatusCommandHandler.Handle [L21–L57]
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method WriteQuery [L34]

