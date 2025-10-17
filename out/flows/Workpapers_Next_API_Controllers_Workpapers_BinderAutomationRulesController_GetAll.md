[web] GET /api/binder-automation-rules/  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.GetAll)  [L41–L50] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to BinderAutomationRuleDto [L44]
    └─ automapper.registration WorkpapersMappingProfile (BinderAutomationRule->BinderAutomationRuleDto) [L1021]
  └─ calls BinderAutomationRuleRepository.ReadQuery [L44]
  └─ query BinderAutomationRule [L44]
    └─ reads_from BinderAutomationRules
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ BinderAutomationRule writes=0 reads=1
    └─ mappings 1
      └─ BinderAutomationRuleDto

