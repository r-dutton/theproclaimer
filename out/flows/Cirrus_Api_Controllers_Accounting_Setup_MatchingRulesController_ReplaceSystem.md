[web] POST /api/accounting/matching-rules/file/{fileId}/replace  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.ReplaceSystem)  [L122–L127] status=201 [auth=user]
  └─ sends_request ReplaceMatchingRulesCommand -> ReplaceMatchingRulesCommandHandler [L126]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ReplaceMatchingRulesCommandHandler.Handle [L29–L69]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L60]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<MatchingRule> (Scoped (inferred))
        └─ method WriteQuery [L48]
          └─ implementation Cirrus.Data.Repository.Accounting.Setup.MatchingRuleRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ ReplaceMatchingRulesCommand
    └─ handlers 1
      └─ ReplaceMatchingRulesCommandHandler

