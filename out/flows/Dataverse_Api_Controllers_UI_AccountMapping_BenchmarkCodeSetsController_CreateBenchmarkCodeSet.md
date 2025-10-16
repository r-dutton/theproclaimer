[web] POST /api/ui/account-mapping/benchmark-code-sets  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.CreateBenchmarkCodeSet)  [L55–L62] status=201 [auth=Authentication.AdminPolicy]
  └─ sends_request CreateBenchmarkCodeSetCommand [L59]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodeSets.CreateBenchmarkCodeSetCommandHandler.Handle [L19–L39]
      └─ uses_service UserService
        └─ method IsMaster [L32]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<BenchmarkCodeSet>
        └─ method Add [L35]
          └─ ... (no implementation details available)

