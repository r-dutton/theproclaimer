[web] POST /api/binder-automation-rules/  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.Create)  [L61–L68] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderAutomationRuleRepository.Add [L65]
  └─ insert BinderAutomationRule [L65]
    └─ reads_from BinderAutomationRules
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ BinderAutomationRule writes=1 reads=0

