[web] GET /api/matching-rule-sets/{id}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.Get)  [L40–L46] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MatchingRuleSetDto [L43]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L889]
    └─ automapper.registration CirrusMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L229]
  └─ calls MatchingRuleSetRepository.ReadQuery [L43]
  └─ queries MatchingRuleSet [L43]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method ReadQuery [L43]

