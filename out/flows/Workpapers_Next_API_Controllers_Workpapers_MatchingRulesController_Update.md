[web] PUT /api/matching-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Update)  [L134–L146] [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleRepository.WriteQuery [L138]
  └─ writes_to MatchingRule [L138]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method WriteQuery [L138]

