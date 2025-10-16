[web] DELETE /api/binder-automation-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.Delete)  [L93–L104] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls BinderAutomationRuleRepository.Remove [L101]
  └─ calls BinderAutomationRuleRepository.WriteQuery [L96]
  └─ delete BinderAutomationRule [L101]
    └─ reads_from BinderAutomationRules
  └─ write BinderAutomationRule [L96]
    └─ reads_from BinderAutomationRules
  └─ uses_service IControlledRepository<BinderAutomationRule>
    └─ method WriteQuery [L96]
      └─ ... (no implementation details available)

