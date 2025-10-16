[web] GET /api/super/tenants/{id}  (Dataverse.Api.Controllers.Super.TenantController.Get)  [L90–L110] status=200 [auth=Authentication.MasterPolicy]
  └─ maps_to TenantDto [L93]
  └─ calls TenantRepository.ReadTable [L93]
  └─ query Tenant [L93]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L93]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ uses_service UserService
    └─ method GetIdentityId [L103]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetIdentityId [L15-L185]

