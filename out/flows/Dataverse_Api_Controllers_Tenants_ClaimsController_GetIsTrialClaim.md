[web] GET /api/claims/isTrial/{tenantId}/product/{productType}  (Dataverse.Api.Controllers.Tenants.ClaimsController.GetIsTrialClaim)  [L115–L125] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ calls TenantRepository.ReadTable [L118]
  └─ query Tenant [L118]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L118]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 1
      └─ TenantRepository

