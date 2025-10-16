[web] POST /api/super/tenants/{id}/audit-sync  (Dataverse.Api.Controllers.Super.TenantController.RequestAuditSync)  [L153–L157] status=201 [auth=Authentication.MasterPolicy]
  └─ sends_request RequestTenantAuditSyncCommand -> RequestTenantAuditSyncCommandHandler [L156]
    └─ handled_by Dataverse.ApplicationService.Commands.Tenants.RequestTenantAuditSyncCommandHandler.Handle [L37–L117]
      └─ calls TenantRepository.WriteTable [L61]
      └─ uses_service MockMessagingService
        └─ method RequestSharePointSiteUpdate [L82]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DataverseEntityFailureLog> (Scoped (inferred))
        └─ method ReadQuery [L70]
          └─ implementation Dataverse.Data.Repository.Base.DataverseEntityFailureLogRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L66]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ requests 1
      └─ RequestTenantAuditSyncCommand
    └─ handlers 1
      └─ RequestTenantAuditSyncCommandHandler
    └─ caches 1
      └─ IMemoryCache

