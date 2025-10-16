[web] POST /api/accounting/matching-rules/file/{fileId}/replace  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.ReplaceSystem)  [L122–L127] status=201 [auth=user]
  └─ sends_request ReplaceMatchingRulesCommand [L126]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ReplaceMatchingRulesCommandHandler.Handle [L29–L69]
      └─ uses_service IControlledRepository<MatchingRule>
        └─ method WriteQuery [L48]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L60]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

