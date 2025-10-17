[web] PUT /api/binder-automation-rules/  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.UpdateAll)  [L83–L91] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request CreateOrUpdateBinderAutomationRules -> CreateOrUpdateBinderAutomationRulesHandler [L86]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.BinderAutomationRules.CreateOrUpdateBinderAutomationRulesHandler.Handle [L24–L82]
      └─ uses_service IControlledRepository<BinderAutomationRule> (Scoped (inferred))
        └─ method WriteQuery [L35]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderAutomationRuleRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ CreateOrUpdateBinderAutomationRules
    └─ handlers 1
      └─ CreateOrUpdateBinderAutomationRulesHandler

