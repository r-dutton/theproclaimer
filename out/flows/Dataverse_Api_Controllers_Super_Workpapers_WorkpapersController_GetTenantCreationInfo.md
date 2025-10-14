[web] GET /api/super/workpapers/{tenantId:Guid}/tenant-creation-info  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.GetTenantCreationInfo)  [L53–L58] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetTenantCreateInfoQuery [L57]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Workpapers.GetTenantCreateInfoQueryHandler.Handle [L38–L81]
      └─ calls TenantRepository.ReadTable [L60]
      └─ uses_client WorkpapersClient [L73]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L79]
      └─ uses_service WorkpapersClient
        └─ method Get [L73]

