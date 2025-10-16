[web] GET /api/taxonomy/  (Workpapers.Next.API.Controllers.Workpapers.TaxonomyController.GetAll)  [L32–L39] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to TaxonomyLightDto [L35]
    └─ automapper.registration WorkpapersMappingProfile (Taxonomy->TaxonomyLightDto) [L880]
  └─ calls TaxonomyRepository.ReadQuery [L35]
  └─ query Taxonomy [L35]
    └─ reads_from Taxonomy
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Taxonomy writes=0 reads=1
    └─ mappings 1
      └─ TaxonomyLightDto

