[web] POST /api/matching-rule-sets/  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.Create)  [L59–L66] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleSetRepository.Add [L63]
  └─ insert MatchingRuleSet [L63]
    └─ reads_from MatchingRuleSets
  └─ uses_service MatchingRuleSetRepository
    └─ method Add [L63]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleSetRepository.Add [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ MatchingRuleSet writes=1 reads=0
    └─ services 1
      └─ MatchingRuleSetRepository

