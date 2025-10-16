[web] PUT /api/matching-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Update)  [L134–L146] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleRepository.WriteQuery [L138]
  └─ write MatchingRule [L138]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method WriteQuery [L138]
      └─ ... (no implementation details available)

