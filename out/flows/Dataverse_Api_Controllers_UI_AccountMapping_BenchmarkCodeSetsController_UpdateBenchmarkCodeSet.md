[web] PUT /api/ui/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.UpdateBenchmarkCodeSet)  [L70–L75] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request UpdateBenchmarkCodeSetCommand [L74]
    └─ handled_by Dataverse.ApplicationService.Commands.AccountMapping.BenchmarkCodeSets.UpdateBenchmarkCodeSetCommandHandler.Handle [L27–L55]
      └─ uses_service UserService
        └─ method IsMaster [L40]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<BenchmarkCodeSet>
        └─ method WriteQuery [L41]
          └─ ... (no implementation details available)

