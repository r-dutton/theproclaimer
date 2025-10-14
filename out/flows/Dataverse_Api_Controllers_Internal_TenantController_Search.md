[web] GET /api/internal/tenants/  (Dataverse.Api.Controllers.Internal.TenantController.Search)  [L42–L51] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request FindTenantsQuery [L50]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Dataverse.Queries.FindTenantsQueryHandler.Handle [L32–L52]
      └─ uses_service DataverseService
        └─ method Get [L50]

