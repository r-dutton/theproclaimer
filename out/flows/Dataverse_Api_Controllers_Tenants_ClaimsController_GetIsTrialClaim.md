[web] GET /api/claims/isTrial/{tenantId}/product/{productType}  (Dataverse.Api.Controllers.Tenants.ClaimsController.GetIsTrialClaim)  [L115–L125] [auth=Authentication.MachineToMachinePolicy]
  └─ calls TenantRepository.ReadTable [L118]
  └─ queries Tenant [L118]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L118]

