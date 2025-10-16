[web] GET /api/tenant/databases/  (Dataverse.Api.Controllers.Tenants.DatabaseController.GetAll)  [L33–L43] status=200 [auth=master]
  └─ maps_to DatabaseLightDto [L36]
  └─ calls TenantRepository.ReadTable [L36]
  └─ query Tenant [L36]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L36]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

