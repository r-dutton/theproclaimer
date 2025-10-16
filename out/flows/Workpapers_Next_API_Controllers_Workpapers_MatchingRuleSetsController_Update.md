[web] PUT /api/matching-rule-sets/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.Update)  [L76–L81] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleSetRepository.WriteQuery [L79]
  └─ write MatchingRuleSet [L79]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method WriteQuery [L79]
      └─ ... (no implementation details available)

