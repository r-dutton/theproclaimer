[web] POST /api/super/tenants/{id}/data-lake-sync  (Dataverse.Api.Controllers.Super.TenantController.RequestDataLakeAuditSync)  [L159–L169] status=201 [auth=Authentication.MasterPolicy]
  └─ calls TenantRepository.ReadTable [L162]
  └─ query Tenant [L162]
    └─ reads_from Tenants
  └─ uses_service MockMessagingService
    └─ method RequestDataLakeSecurityUpdate [L168]
      └─ ... (no implementation details available)
  └─ uses_service TenantRepository
    └─ method ReadTable [L162]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

