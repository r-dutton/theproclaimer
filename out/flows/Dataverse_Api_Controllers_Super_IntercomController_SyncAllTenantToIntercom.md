[web] PUT /api/super/intercom/sync-all  (Dataverse.Api.Controllers.Super.IntercomController.SyncAllTenantToIntercom)  [L36–L45] [auth=Authentication.MasterPolicy]
  └─ calls TenantRepository.ReadTable [L39]
  └─ queries Tenant [L39]
    └─ reads_from Tenants
  └─ uses_service MockMessagingService
    └─ method RequestAllTenantsIntercomSync [L44]
  └─ uses_service TenantRepository
    └─ method ReadTable [L39]

