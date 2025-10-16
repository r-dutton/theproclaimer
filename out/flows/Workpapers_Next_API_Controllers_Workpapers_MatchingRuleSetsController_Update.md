[web] PUT /api/matching-rule-sets/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.Update)  [L76–L81] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleSetRepository.WriteQuery [L79]
  └─ write MatchingRuleSet [L79]
    └─ reads_from MatchingRuleSets
  └─ uses_service MatchingRuleSetRepository
    └─ method WriteQuery [L79]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleSetRepository.WriteQuery [L8-L38]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ MatchingRuleSet writes=1 reads=0
    └─ services 1
      └─ MatchingRuleSetRepository

