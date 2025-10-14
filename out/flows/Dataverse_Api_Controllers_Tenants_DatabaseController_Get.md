[web] GET /api/tenant/databases/{databaseId}  (Dataverse.Api.Controllers.Tenants.DatabaseController.Get)  [L45–L52] [auth=master]
  └─ maps_to DatabaseDto [L48]
  └─ calls TenantRepository.ReadTable [L48]
  └─ queries Tenant [L48]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L48]

