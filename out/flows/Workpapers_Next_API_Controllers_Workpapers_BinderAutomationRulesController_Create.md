[web] POST /api/binder-automation-rules/  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.Create)  [L61–L68] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderAutomationRuleRepository.Add [L65]
  └─ insert BinderAutomationRule [L65]
    └─ reads_from BinderAutomationRules
  └─ uses_service IControlledRepository<BinderAutomationRule>
    └─ method Add [L65]
      └─ ... (no implementation details available)

