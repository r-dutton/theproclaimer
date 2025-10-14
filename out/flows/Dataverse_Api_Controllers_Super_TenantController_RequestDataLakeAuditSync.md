[web] POST /api/super/tenants/{id}/data-lake-sync  (Dataverse.Api.Controllers.Super.TenantController.RequestDataLakeAuditSync)  [L159–L169] [auth=Authentication.MasterPolicy]
  └─ calls TenantRepository.ReadTable [L162]
  └─ queries Tenant [L162]
    └─ reads_from Tenants
  └─ uses_service MockMessagingService
    └─ method RequestDataLakeSecurityUpdate [L168]
  └─ uses_service TenantRepository
    └─ method ReadTable [L162]

