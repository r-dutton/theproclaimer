[web] DELETE /api/ui/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.DeleteBenchmarkCodeSet)  [L82–L89] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request DeleteBenchmarkCodeSetCommand [L86]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodeSets.DeleteBenchmarkCodeSetCommandHandler.Handle [L24–L53]
      └─ uses_service UserService
        └─ method IsMaster [L39]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<BenchmarkCodeSet>
        └─ method WriteQuery [L37]
          └─ ... (no implementation details available)

