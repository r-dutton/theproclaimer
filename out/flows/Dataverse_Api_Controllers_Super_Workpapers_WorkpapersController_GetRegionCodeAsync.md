[web] GET /api/super/workpapers  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.GetRegionCodeAsync)  [L145–L148] status=200 [auth=Authentication.MasterPolicy]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L147]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]

