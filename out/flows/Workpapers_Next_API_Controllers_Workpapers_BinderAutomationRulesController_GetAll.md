[web] GET /api/binder-automation-rules/  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.GetAll)  [L41–L50] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to BinderAutomationRuleDto [L44]
    └─ automapper.registration WorkpapersMappingProfile (BinderAutomationRule->BinderAutomationRuleDto) [L1021]
  └─ calls BinderAutomationRuleRepository.ReadQuery [L44]
  └─ query BinderAutomationRule [L44]
    └─ reads_from BinderAutomationRules
  └─ uses_service IControlledRepository<BinderAutomationRule>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

