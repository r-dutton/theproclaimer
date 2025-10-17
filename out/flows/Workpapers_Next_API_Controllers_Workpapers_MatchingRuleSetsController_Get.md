[web] GET /api/matching-rule-sets/{id}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.Get)  [L40–L46] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MatchingRuleSetDto [L43]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L889]
  └─ calls MatchingRuleSetRepository.ReadQuery [L43]
  └─ query MatchingRuleSet [L43]
    └─ reads_from MatchingRuleSets
  └─ uses_service MatchingRuleSetRepository
    └─ method ReadQuery [L43]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleSetRepository.ReadQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatchingRuleSet writes=0 reads=1
    └─ services 1
      └─ MatchingRuleSetRepository
    └─ mappings 1
      └─ MatchingRuleSetDto

