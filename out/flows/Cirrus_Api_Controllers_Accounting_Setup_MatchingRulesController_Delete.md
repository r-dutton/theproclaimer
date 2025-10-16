[web] DELETE /api/accounting/matching-rules/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Delete)  [L200–L208] status=200 [auth=user]
  └─ calls MatchingRuleRepository.Remove [L205]
  └─ calls MatchingRuleRepository.WriteQuery [L203]
  └─ delete MatchingRule [L205]
    └─ reads_from MatchingRules
  └─ write MatchingRule [L203]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method WriteQuery [L203]
      └─ ... (no implementation details available)

