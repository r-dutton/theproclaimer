[web] GET /api/super/workpapers/{tenantId:Guid}/tenant-creation-info  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.GetTenantCreationInfo)  [L53–L58] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetTenantCreateInfoQuery [L57]
    └─ handled_by Dataverse.ApplicationService.Queries.Workpapers.GetTenantCreateInfoQueryHandler.Handle [L38–L81]
      └─ calls TenantRepository.ReadTable [L60]
      └─ uses_client WorkpapersClient [L73]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L79]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service WorkpapersClient
        └─ method Get [L73]
          └─ ... (no implementation details available)

