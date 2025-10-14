[web] POST /api/matching-rules/  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Create)  [L113–L129] [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleRepository.Add [L126]
  └─ calls MatchingRuleRepository.WriteQuery [L120]
  └─ writes_to MatchingRule [L126]
    └─ reads_from MatchingRules
  └─ writes_to MatchingRule [L120]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method WriteQuery [L120]

