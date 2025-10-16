[web] POST /api/super/tenants/{id}/audit-sync  (Dataverse.Api.Controllers.Super.TenantController.RequestAuditSync)  [L153–L157] status=201 [auth=Authentication.MasterPolicy]
  └─ sends_request RequestTenantAuditSyncCommand [L156]
    └─ handled_by Dataverse.ApplicationService.Commands.Tenants.RequestTenantAuditSyncCommandHandler.Handle [L37–L117]
      └─ calls TenantRepository.WriteTable [L61]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L66]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service MockMessagingService
        └─ method RequestSharePointSiteUpdate [L82]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DataverseEntityFailureLog>
        └─ method ReadQuery [L70]
          └─ ... (no implementation details available)

