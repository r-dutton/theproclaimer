[web] GET /api/accounting/tax/taxonomy/  (Cirrus.Api.Controllers.Accounting.Tax.TaxonomyController.GetAll)  [L36–L45] status=200 [auth=user]
  └─ maps_to TaxonomyLightDto [L39]
  └─ uses_service IReadRepository (InstancePerLifetimeScope)
    └─ method Table [L39]
      └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 1
      └─ IReadRepository
    └─ mappings 1
      └─ TaxonomyLightDto

