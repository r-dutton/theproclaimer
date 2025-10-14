[web] DELETE /api/ui/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.DeleteBenchmarkCodeSet)  [L82–L89] [auth=Authentication.AdminPolicy]
  └─ sends_request DeleteBenchmarkCodeSetCommand [L86]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodeSets.DeleteBenchmarkCodeSetCommandHandler.Handle [L24–L53]
      └─ uses_service IControlledRepository<BenchmarkCodeSet>
        └─ method WriteQuery [L37]
      └─ uses_service UserService
        └─ method IsMaster [L39]

