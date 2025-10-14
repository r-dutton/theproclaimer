[web] DELETE /api/accounting/matching-rules/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Delete)  [L200–L208] [auth=user]
  └─ calls MatchingRuleRepository.Remove [L205]
  └─ calls MatchingRuleRepository.WriteQuery [L203]
  └─ writes_to MatchingRule [L205]
    └─ reads_from MatchingRules
  └─ writes_to MatchingRule [L203]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method WriteQuery [L203]

