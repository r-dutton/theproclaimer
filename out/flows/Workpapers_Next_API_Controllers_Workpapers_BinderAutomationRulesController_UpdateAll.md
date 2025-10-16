[web] PUT /api/binder-automation-rules/  (Workpapers.Next.API.Controllers.Workpapers.BinderAutomationRulesController.UpdateAll)  [L83–L91] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service IMapper
    └─ method Map [L88]
      └─ ... (no implementation details available)
  └─ sends_request CreateOrUpdateBinderAutomationRules [L86]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.BinderAutomationRules.CreateOrUpdateBinderAutomationRulesHandler.Handle [L24–L82]
      └─ uses_service IControlledRepository<BinderAutomationRule>
        └─ method WriteQuery [L35]
          └─ ... (no implementation details available)

