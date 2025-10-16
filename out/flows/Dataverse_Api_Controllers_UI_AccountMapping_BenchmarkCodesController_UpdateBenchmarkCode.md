[web] PUT /api/ui/account-mapping/benchmark-code-sets/benchmark-codes/{benchmarkCodeSetId:guid}/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.UpdateBenchmarkCode)  [L78–L83] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request UpdateBenchmarkCodeCommand [L82]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodes.UpdateBenchmarkCodeCommandHandler.Handle [L30–L60]
      └─ uses_service UserService
        └─ method IsMaster [L43]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<BenchmarkCode>
        └─ method WriteQuery [L44]
          └─ ... (no implementation details available)

