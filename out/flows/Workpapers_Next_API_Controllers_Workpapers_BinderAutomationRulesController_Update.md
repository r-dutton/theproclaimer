[web] PUT /api/binder-automation-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.Update)  [L70–L81] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderAutomationRuleRepository.WriteQuery [L73]
  └─ write BinderAutomationRule [L73]
    └─ reads_from BinderAutomationRules
  └─ uses_service IControlledRepository<BinderAutomationRule>
    └─ method WriteQuery [L73]
      └─ ... (no implementation details available)

