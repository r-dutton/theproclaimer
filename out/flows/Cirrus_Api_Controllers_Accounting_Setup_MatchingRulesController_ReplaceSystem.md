[web] POST /api/accounting/matching-rules/file/{fileId}/replace  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.ReplaceSystem)  [L122–L127] [auth=user]
  └─ sends_request ReplaceMatchingRulesCommand [L126]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ReplaceMatchingRulesCommandHandler.Handle [L29–L69]
      └─ uses_service IControlledRepository<MatchingRule>
        └─ method WriteQuery [L48]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L60]

