[web] DELETE /api/binder-automation-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.Delete)  [L93–L104] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderAutomationRuleRepository (methods: Remove,WriteQuery) [L101]
  └─ delete BinderAutomationRule [L101]
    └─ reads_from BinderAutomationRules
  └─ write BinderAutomationRule [L96]
    └─ reads_from BinderAutomationRules
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ BinderAutomationRule writes=2 reads=0

