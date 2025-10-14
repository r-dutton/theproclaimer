[web] POST /api/ui/account-mapping/benchmark-code-sets  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.CreateBenchmarkCodeSet)  [L55–L62] [auth=Authentication.AdminPolicy]
  └─ sends_request CreateBenchmarkCodeSetCommand [L59]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodeSets.CreateBenchmarkCodeSetCommandHandler.Handle [L19–L39]
      └─ uses_service IControlledRepository<BenchmarkCodeSet>
        └─ method Add [L35]
      └─ uses_service UserService
        └─ method IsMaster [L32]

