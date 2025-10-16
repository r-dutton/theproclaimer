[web] GET /api/ui/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.GetBenchmarkCodeSet)  [L31–L36] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetBenchmarkCodeSetLightQuery -> GetBenchmarkCodeSetQueryHandler [L35]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodeSets.GetBenchmarkCodeSetQueryHandler.Handle [L37–L64]
      └─ maps_to BenchmarkCodeSetDto [L59]
        └─ automapper.registration DataverseMappingProfile (BenchmarkCodeSet->BenchmarkCodeSetDto) [L248]
      └─ maps_to BenchmarkCodeSetLightDto [L51]
        └─ automapper.registration DataverseMappingProfile (BenchmarkCodeSet->BenchmarkCodeSetLightDto) [L247]
      └─ uses_service IControlledRepository<BenchmarkCodeSet> (Scoped (inferred))
        └─ method ReadQuery [L51]
          └─ implementation Dataverse.Data.Repository.AccountMapping.BenchmarkCodeSetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetBenchmarkCodeSetLightQuery
    └─ handlers 1
      └─ GetBenchmarkCodeSetQueryHandler
    └─ mappings 2
      └─ BenchmarkCodeSetDto
      └─ BenchmarkCodeSetLightDto

