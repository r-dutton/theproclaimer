[web] GET /api/template-standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.TemplateStandardAccountsController.GetTemplateStandardAccount)  [L45–L53] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to TemplateStandardAccountDto [L49]
    └─ automapper.registration WorkpapersMappingProfile (TemplateStandardAccount->TemplateStandardAccountDto) [L674]
  └─ calls TemplateStandardAccountRepository.ReadQuery [L49]
  └─ query TemplateStandardAccount [L49]
    └─ reads_from TemplateStandardAccounts
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TemplateStandardAccount writes=0 reads=1
    └─ mappings 1
      └─ TemplateStandardAccountDto

