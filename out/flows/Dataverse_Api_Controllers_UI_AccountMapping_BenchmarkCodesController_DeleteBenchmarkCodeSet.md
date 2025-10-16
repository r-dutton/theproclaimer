[web] DELETE /api/ui/account-mapping/benchmark-code-sets/benchmark-codes/{benchmarkCodeSetId:guid}/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.DeleteBenchmarkCodeSet)  [L91–L98] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request DeleteBenchmarkCodeCommand [L95]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodes.DeleteBenchmarkCodeCommandHandler.Handle [L27–L58]
      └─ uses_service UserService
        └─ method IsMaster [L44]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<BenchmarkCode>
        └─ method WriteQuery [L40]
          └─ ... (no implementation details available)

