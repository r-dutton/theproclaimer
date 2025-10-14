[web] POST /api/super/tenants/{id}/audit-sync  (Dataverse.Api.Controllers.Super.TenantController.RequestAuditSync)  [L153–L157] [auth=Authentication.MasterPolicy]
  └─ sends_request RequestTenantAuditSyncCommand [L156]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Tenants.RequestTenantAuditSyncCommandHandler.Handle [L37–L117]
      └─ calls TenantRepository.WriteTable [L61]
      └─ uses_service IControlledRepository<DataverseEntityFailureLog>
        └─ method ReadQuery [L70]
      └─ uses_service MockMessagingService
        └─ method RequestSharePointSiteUpdate [L82]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L66]

