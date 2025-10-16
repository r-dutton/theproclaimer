[web] GET /api/binder-automation-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.Get)  [L52–L59] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to BinderAutomationRuleDto [L55]
    └─ automapper.registration WorkpapersMappingProfile (BinderAutomationRule->BinderAutomationRuleDto) [L1021]
  └─ calls BinderAutomationRuleRepository.ReadQuery [L55]
  └─ query BinderAutomationRule [L55]
    └─ reads_from BinderAutomationRules
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderAutomationRule writes=0 reads=1
    └─ mappings 1
      └─ BinderAutomationRuleDto

