[web] GET /api/matching-rule-sets/  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.GetAll)  [L48–L57] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MatchingRuleSetDto [L51]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L889]
    └─ automapper.registration CirrusMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L229]
  └─ calls MatchingRuleSetRepository.ReadQuery [L51]
  └─ query MatchingRuleSet [L51]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method ReadQuery [L51]
      └─ ... (no implementation details available)

