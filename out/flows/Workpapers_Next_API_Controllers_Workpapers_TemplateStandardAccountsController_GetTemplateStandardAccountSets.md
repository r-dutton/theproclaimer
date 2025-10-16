[web] GET /api/template-standard-accounts/sets  (Workpapers.Next.API.Controllers.Workpapers.TemplateStandardAccountsController.GetTemplateStandardAccountSets)  [L76–L85] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to TemplateStandardAccountSetDto [L80]
    └─ automapper.registration WorkpapersMappingProfile (TemplateStandardAccountSet->TemplateStandardAccountSetDto) [L672]
  └─ calls TemplateStandardAccountSetRepository.ReadQuery [L80]
  └─ query TemplateStandardAccountSet [L80]
    └─ reads_from TemplateStandardAccountSets
  └─ uses_service IControlledRepository<TemplateStandardAccountSet>
    └─ method ReadQuery [L80]
      └─ ... (no implementation details available)

