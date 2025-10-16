[web] PUT /api/custom-statuses/{id:Guid}/reorder  (Workpapers.Next.API.Controllers.CustomStatusesController.UpdateOrder)  [L141–L147] status=200
  └─ sends_request ReorderCustomStatusCommand [L144]
    └─ handled_by Workpapers.Next.Data.Commands.Dataverse.ReorderCustomStatusCommandandler.Handle [L35–L88]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L65]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
      └─ uses_service DataverseService
        └─ method Put [L67]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.Put [L17-L127]
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<CustomStatus> (AddScoped)
        └─ method ReadQuery [L58]
          └─ ... (no implementation details available)

