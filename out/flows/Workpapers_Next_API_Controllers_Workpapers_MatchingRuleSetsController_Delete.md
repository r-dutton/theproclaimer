[web] DELETE /api/matching-rule-sets/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.Delete)  [L86–L91] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleSetRepository (methods: Remove,WriteQuery) [L90]
  └─ delete MatchingRuleSet [L90]
    └─ reads_from MatchingRuleSets
  └─ write MatchingRuleSet [L89]
    └─ reads_from MatchingRuleSets
  └─ uses_service MatchingRuleSetRepository
    └─ method WriteQuery [L89]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleSetRepository.WriteQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ MatchingRuleSet writes=2 reads=0
    └─ services 1
      └─ MatchingRuleSetRepository

