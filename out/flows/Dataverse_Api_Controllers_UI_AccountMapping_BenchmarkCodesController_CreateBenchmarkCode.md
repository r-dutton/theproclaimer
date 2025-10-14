[web] POST /api/ui/account-mapping/benchmark-code-sets/benchmark-codes/{benchmarkCodeSetId:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.CreateBenchmarkCode)  [L62–L69] [auth=Authentication.AdminPolicy]
  └─ sends_request CreateBenchmarkCodeCommand [L66]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodes.CreateBenchmarkCodeCommandHandler.Handle [L26–L46]
      └─ uses_service IControlledRepository<BenchmarkCode>
        └─ method Add [L42]
      └─ uses_service UserService
        └─ method IsMaster [L39]

