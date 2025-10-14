[web] PUT /api/matching-rules/{id}/reorder  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Reorder)  [L154–L175] [auth=AuthorizationPolicies.Administrator,admin]
  └─ calls MatchingRuleRepository.WriteQuery [L166]
  └─ calls MatchingRuleRepository.ReadQuery [L158]
  └─ queries MatchingRule [L158]
    └─ reads_from MatchingRules
  └─ writes_to MatchingRule [L166]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method ReadQuery [L158]

