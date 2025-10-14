[web] DELETE /api/ui/account-mapping/benchmark-code-sets/benchmark-codes/{benchmarkCodeSetId:guid}/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.DeleteBenchmarkCodeSet)  [L91–L98] [auth=Authentication.AdminPolicy]
  └─ sends_request DeleteBenchmarkCodeCommand [L95]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodes.DeleteBenchmarkCodeCommandHandler.Handle [L27–L58]
      └─ uses_service IControlledRepository<BenchmarkCode>
        └─ method WriteQuery [L40]
      └─ uses_service UserService
        └─ method IsMaster [L44]

