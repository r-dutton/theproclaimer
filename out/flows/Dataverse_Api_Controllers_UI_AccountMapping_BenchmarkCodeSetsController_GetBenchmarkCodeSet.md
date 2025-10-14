[web] GET /api/ui/account-mapping/benchmark-code-sets/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.BenchmarkCodeSetsController.GetBenchmarkCodeSet)  [L31–L36] [auth=Authentication.UserPolicy]
  └─ sends_request GetBenchmarkCodeSetLightQuery [L35]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.BenchmarkCodeSets.GetBenchmarkCodeSetQueryHandler.Handle [L37–L64]
      └─ maps_to BenchmarkCodeSetDto [L59]
        └─ automapper.registration DataverseMappingProfile (BenchmarkCodeSet->BenchmarkCodeSetDto) [L247]
      └─ maps_to BenchmarkCodeSetLightDto [L51]
        └─ automapper.registration DataverseMappingProfile (BenchmarkCodeSet->BenchmarkCodeSetLightDto) [L246]
      └─ uses_service IControlledRepository<BenchmarkCodeSet>
        └─ method ReadQuery [L51]

