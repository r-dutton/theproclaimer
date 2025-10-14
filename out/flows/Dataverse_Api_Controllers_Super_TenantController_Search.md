[web] GET /api/super/tenants/  (Dataverse.Api.Controllers.Super.TenantController.Search)  [L74–L88] [auth=Authentication.MasterPolicy]
  └─ sends_request FindTenantsQuery [L86]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Dataverse.Queries.FindTenantsQueryHandler.Handle [L32–L52]
      └─ uses_service DataverseService
        └─ method Get [L50]

