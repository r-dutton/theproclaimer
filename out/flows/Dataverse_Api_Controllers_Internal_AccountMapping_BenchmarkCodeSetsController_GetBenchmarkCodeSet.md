[web] GET /api/internal/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.Internal.AccountMapping.BenchmarkCodeSetsController.GetBenchmarkCodeSet)  [L31–L35] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetBenchmarkCodeSetQuery [L34]
    └─ handled_by Cirrus.Central.Dataverse.Queries.AccountMapping.GetBenchmarkCodeSetQueryHandler.Handle [L27–L62]
      └─ uses_service UserService
        └─ method GetUser [L42]
          └─ implementation Cirrus.ApplicationService.Services.UserService.GetUser [L14-L122]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L49]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
      └─ uses_service DataverseService
        └─ method Get [L50]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]
  └─ returns DataverseBenchmarkCodeSetDto [L34] [return]

