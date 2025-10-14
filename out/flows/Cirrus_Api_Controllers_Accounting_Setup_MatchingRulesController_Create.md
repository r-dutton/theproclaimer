[web] POST /api/accounting/matching-rules/  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Create)  [L144–L156] [auth=user]
  └─ calls MatchingRuleRepository.Add [L154]
  └─ calls MatchingRuleRepository.WriteQuery [L148]
  └─ writes_to MatchingRule [L154]
    └─ reads_from MatchingRules
  └─ writes_to MatchingRule [L148]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method WriteQuery [L148]

