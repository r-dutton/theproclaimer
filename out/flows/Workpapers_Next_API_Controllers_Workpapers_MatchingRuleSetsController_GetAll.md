[web] GET /api/matching-rule-sets/  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.GetAll)  [L48–L57] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MatchingRuleSetDto [L51]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L889]
  └─ calls MatchingRuleSetRepository.ReadQuery [L51]
  └─ query MatchingRuleSet [L51]
    └─ reads_from MatchingRuleSets
  └─ uses_service MatchingRuleSetRepository
    └─ method ReadQuery [L51]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleSetRepository.ReadQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatchingRuleSet writes=0 reads=1
    └─ services 1
      └─ MatchingRuleSetRepository
    └─ mappings 1
      └─ MatchingRuleSetDto

