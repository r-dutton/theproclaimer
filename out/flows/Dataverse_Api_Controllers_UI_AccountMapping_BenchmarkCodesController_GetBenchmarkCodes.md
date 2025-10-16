[web] GET /api/ui/account-mapping/benchmark-code-sets/benchmark-codes  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodesController.GetBenchmarkCodes)  [L46–L53] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetBenchmarkCodesQuery [L52]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodes.GetBenchmarkCodesQueryHandler.Handle [L30–L60]
      └─ maps_to BenchmarkCodeDto [L55]
      └─ uses_service IControlledRepository<BenchmarkCode>
        └─ method ReadQuery [L43]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L55]
          └─ ... (no implementation details available)

