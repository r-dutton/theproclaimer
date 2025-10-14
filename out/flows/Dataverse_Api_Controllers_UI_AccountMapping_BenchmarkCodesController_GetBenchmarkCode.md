[web] GET /api/ui/account-mapping/benchmark-code-sets/benchmark-codes/{benchmarkCodeSetId:guid}/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.GetBenchmarkCode)  [L32–L37] [auth=Authentication.UserPolicy]
  └─ sends_request GetBenchmarkCodeQuery [L36]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodes.GetBenchmarkCodeQueryHandler.Handle [L28–L47]
      └─ maps_to BenchmarkCodeDto [L41]
        └─ automapper.registration DataverseMappingProfile (BenchmarkCode->BenchmarkCodeDto) [L244]
      └─ uses_service IControlledRepository<BenchmarkCode>
        └─ method ReadQuery [L41]

