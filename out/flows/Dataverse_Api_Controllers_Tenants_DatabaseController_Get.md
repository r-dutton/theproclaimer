[web] GET /api/tenant/databases/{databaseId}  (Dataverse.Api.Controllers.Tenants.DatabaseController.Get)  [L45–L52] status=200 [auth=master]
  └─ maps_to DatabaseDto [L48]
  └─ calls TenantRepository.ReadTable [L48]
  └─ query Tenant [L48]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L48]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

