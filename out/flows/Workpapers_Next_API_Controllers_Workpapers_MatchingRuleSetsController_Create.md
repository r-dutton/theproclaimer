[web] POST /api/matching-rule-sets/  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.Create)  [L59–L66] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleSetRepository.Add [L63]
  └─ insert MatchingRuleSet [L63]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method Add [L63]
      └─ ... (no implementation details available)

