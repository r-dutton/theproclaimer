[web] GET /api/ui/account-mapping/benchmark-code-sets  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.GetBenchmarkCodeSets)  [L43–L48] [auth=Authentication.UserPolicy]
  └─ sends_request GetBenchmarkCodeSetsQuery [L47]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodeSets.GetBenchmarkCodeSetsQueryHandler.Handle [L24–L43]
      └─ maps_to BenchmarkCodeSetLightDto [L37]
        └─ automapper.registration DataverseMappingProfile (BenchmarkCodeSet->BenchmarkCodeSetLightDto) [L246]
      └─ uses_service IControlledRepository<BenchmarkCodeSet>
        └─ method ReadQuery [L37]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L38]

