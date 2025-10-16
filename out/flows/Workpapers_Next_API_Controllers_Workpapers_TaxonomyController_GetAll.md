[web] GET /api/taxonomy/  (Workpapers.Next.API.Controllers.Workpapers.TaxonomyController.GetAll)  [L32–L39] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to TaxonomyLightDto [L35]
    └─ automapper.registration CirrusMappingProfile (Taxonomy->TaxonomyLightDto) [L522]
    └─ automapper.registration WorkpapersMappingProfile (Taxonomy->TaxonomyLightDto) [L880]
  └─ calls TaxonomyRepository.ReadQuery [L35]
  └─ query Taxonomy [L35]
    └─ reads_from Taxonomy
  └─ uses_service IControlledRepository<Taxonomy>
    └─ method ReadQuery [L35]
      └─ ... (no implementation details available)

