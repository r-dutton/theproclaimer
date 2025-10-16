[web] GET /api/ui/account-mapping/benchmark-code-sets/benchmark-codes  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.GetBenchmarkCodes)  [L46–L53] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetBenchmarkCodesQuery -> GetBenchmarkCodesQueryHandler [L52]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodes.GetBenchmarkCodesQueryHandler.Handle [L30–L60]
      └─ maps_to BenchmarkCodeDto [L55]
      └─ uses_service IControlledRepository<BenchmarkCode> (Scoped (inferred))
        └─ method ReadQuery [L43]
          └─ implementation Dataverse.Data.Repository.AccountMapping.BenchmarkCodeRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetBenchmarkCodesQuery
    └─ handlers 1
      └─ GetBenchmarkCodesQueryHandler
    └─ mappings 1
      └─ BenchmarkCodeDto

