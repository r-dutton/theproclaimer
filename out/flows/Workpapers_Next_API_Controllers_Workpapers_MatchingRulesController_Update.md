[web] PUT /api/matching-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Update)  [L134–L146] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleRepository.WriteQuery [L138]
  └─ write MatchingRule [L138]
    └─ reads_from MatchingRules
  └─ uses_service MatchingRuleRepository
    └─ method WriteQuery [L138]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleRepository.WriteQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ MatchingRule writes=1 reads=0
    └─ services 1
      └─ MatchingRuleRepository

