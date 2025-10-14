[web] PUT /api/ui/account-mapping/benchmark-code-sets/benchmark-codes/{benchmarkCodeSetId:guid}/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.UpdateBenchmarkCode)  [L78–L83] [auth=Authentication.AdminPolicy]
  └─ sends_request UpdateBenchmarkCodeCommand [L82]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodes.UpdateBenchmarkCodeCommandHandler.Handle [L30–L60]
      └─ uses_service IControlledRepository<BenchmarkCode>
        └─ method WriteQuery [L44]
      └─ uses_service UserService
        └─ method IsMaster [L43]

