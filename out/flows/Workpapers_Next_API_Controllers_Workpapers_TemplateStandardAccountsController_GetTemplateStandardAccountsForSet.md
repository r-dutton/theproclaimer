[web] GET /api/template-standard-accounts/for-set/{setId:guid}  (Workpapers.Next.API.Controllers.Workpapers.TemplateStandardAccountsController.GetTemplateStandardAccountsForSet)  [L60–L70] [auth=AuthorizationPolicies.User]
  └─ maps_to TemplateStandardAccountDto [L64]
    └─ automapper.registration WorkpapersMappingProfile (TemplateStandardAccount->TemplateStandardAccountDto) [L674]
  └─ calls TemplateStandardAccountRepository.ReadQuery [L64]
  └─ queries TemplateStandardAccount [L64]
    └─ reads_from TemplateStandardAccounts
  └─ uses_service IControlledRepository<TemplateStandardAccount>
    └─ method ReadQuery [L64]

