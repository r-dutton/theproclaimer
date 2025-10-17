[web] PUT /api/matching-rules/{id}/reorder  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Reorder)  [L154–L175] status=200 [auth=AuthorizationPolicies.Administrator,admin]
  └─ calls MatchingRuleRepository (methods: WriteQuery,ReadQuery) [L166]
  └─ write MatchingRule [L166]
    └─ reads_from MatchingRules
  └─ query MatchingRule [L158]
    └─ reads_from MatchingRules
  └─ uses_service MatchingRuleRepository
    └─ method ReadQuery [L158]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleRepository.ReadQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ MatchingRule writes=1 reads=1
    └─ services 1
      └─ MatchingRuleRepository

