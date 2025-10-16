[web] POST /api/matching-rules/  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Create)  [L113–L129] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleRepository.Add [L126]
  └─ calls MatchingRuleRepository.WriteQuery [L120]
  └─ insert MatchingRule [L126]
    └─ reads_from MatchingRules
  └─ write MatchingRule [L120]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method WriteQuery [L120]
      └─ ... (no implementation details available)

