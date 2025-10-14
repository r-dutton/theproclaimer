[web] GET /api/internal/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.Internal.AccountMapping.BenchmarkCodeSetsController.GetBenchmarkCodeSet)  [L31–L35] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetBenchmarkCodeSetQuery [L34]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Dataverse.Queries.AccountMapping.GetBenchmarkCodeSetQueryHandler.Handle [L27–L62]
      └─ uses_service DataverseService
        └─ method Get [L50]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L49]
      └─ uses_service UserService
        └─ method GetUser [L42]
  └─ returns DataverseBenchmarkCodeSetDto [L34] [return]

