[web] DELETE /api/matching-rule-sets/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.Delete)  [L86–L91] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleSetRepository.Remove [L90]
  └─ calls MatchingRuleSetRepository.WriteQuery [L89]
  └─ delete MatchingRuleSet [L90]
    └─ reads_from MatchingRuleSets
  └─ write MatchingRuleSet [L89]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method WriteQuery [L89]
      └─ ... (no implementation details available)

