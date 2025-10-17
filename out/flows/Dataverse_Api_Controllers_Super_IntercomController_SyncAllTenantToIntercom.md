[web] PUT /api/super/intercom/sync-all  (Dataverse.Api.Controllers.Super.IntercomController.SyncAllTenantToIntercom)  [L36–L45] status=200 [auth=Authentication.MasterPolicy]
  └─ calls TenantRepository.ReadTable [L39]
  └─ query Tenant [L39]
    └─ reads_from Tenants
  └─ uses_service MockMessagingService
    └─ method RequestAllTenantsIntercomSync [L44]
      └─ ... (no implementation details available)
  └─ uses_service TenantRepository
    └─ method ReadTable [L39]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 2
      └─ MockMessagingService
      └─ TenantRepository

