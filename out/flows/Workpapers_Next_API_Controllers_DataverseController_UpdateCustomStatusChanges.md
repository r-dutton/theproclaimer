[web] PUT /api/dataverse/custom-statuses  (Workpapers.Next.API.Controllers.DataverseController.UpdateCustomStatusChanges)  [L126–L132] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L130]
      └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
  └─ sends_request ProcessDataverseCustomStatusCommand [L131]
    └─ handled_by Workpapers.Next.Data.Commands.Dataverse.ProcessDataverseCustomStatusCommandHandler.Handle [L21–L57]
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method WriteQuery [L34]
          └─ ... (no implementation details available)

