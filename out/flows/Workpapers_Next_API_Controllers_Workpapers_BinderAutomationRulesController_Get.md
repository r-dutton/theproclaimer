[web] GET /api/binder-automation-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.Get)  [L52–L59] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to BinderAutomationRuleDto [L55]
    └─ automapper.registration WorkpapersMappingProfile (BinderAutomationRule->BinderAutomationRuleDto) [L1021]
  └─ calls BinderAutomationRuleRepository.ReadQuery [L55]
  └─ query BinderAutomationRule [L55]
    └─ reads_from BinderAutomationRules
  └─ uses_service IControlledRepository<BinderAutomationRule>
    └─ method ReadQuery [L55]
      └─ ... (no implementation details available)

