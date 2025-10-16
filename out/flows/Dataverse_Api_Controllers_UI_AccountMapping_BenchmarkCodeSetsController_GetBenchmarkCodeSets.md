[web] GET /api/ui/account-mapping/benchmark-code-sets  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.GetBenchmarkCodeSets)  [L43–L48] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetBenchmarkCodeSetsQuery -> GetBenchmarkCodeSetsQueryHandler [L47]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodeSets.GetBenchmarkCodeSetsQueryHandler.Handle [L24–L43]
      └─ maps_to BenchmarkCodeSetLightDto [L37]
        └─ automapper.registration DataverseMappingProfile (BenchmarkCodeSet->BenchmarkCodeSetLightDto) [L247]
      └─ uses_service IControlledRepository<BenchmarkCodeSet> (Scoped (inferred))
        └─ method ReadQuery [L37]
          └─ implementation Dataverse.Data.Repository.AccountMapping.BenchmarkCodeSetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetBenchmarkCodeSetsQuery
    └─ handlers 1
      └─ GetBenchmarkCodeSetsQueryHandler
    └─ mappings 1
      └─ BenchmarkCodeSetLightDto

