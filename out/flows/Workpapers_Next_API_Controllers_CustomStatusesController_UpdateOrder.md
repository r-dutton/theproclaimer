[web] PUT /api/custom-statuses/{id:Guid}/reorder  (Workpapers.Next.API.Controllers.CustomStatusesController.UpdateOrder)  [L137–L143]
  └─ sends_request ReorderCustomStatusCommand [L140]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Commands.Dataverse.ReorderCustomStatusCommandandler.Handle [L35–L88]
      └─ uses_service DataverseService
        └─ method Put [L67]
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method ReadQuery [L62]
      └─ uses_service IControlledRepository<CustomStatus> (AddScoped)
        └─ method ReadQuery [L58]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L65]

