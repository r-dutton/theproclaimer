[web] POST /api/internal/storage/start-zip  (Dataverse.Api.Controllers.Internal.StorageController.StartCreateZipFile)  [L96–L142] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L99]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]

