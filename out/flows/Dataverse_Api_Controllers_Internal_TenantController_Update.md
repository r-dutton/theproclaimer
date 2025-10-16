[web] PUT /api/internal/tenants/{id}  (Dataverse.Api.Controllers.Internal.TenantController.Update)  [L70–L78] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ calls TenantRepository.WriteTable [L73]
  └─ write Tenant [L73]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L73]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.WriteTable [L15-L180]

