[web] POST /api/matching-rules/  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Create)  [L113–L129] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleRepository (methods: Add,WriteQuery) [L126]
  └─ insert MatchingRule [L126]
    └─ reads_from MatchingRules
  └─ write MatchingRule [L120]
    └─ reads_from MatchingRules
  └─ uses_service MatchingRuleRepository
    └─ method WriteQuery [L120]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleRepository.WriteQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ MatchingRule writes=2 reads=0
    └─ services 1
      └─ MatchingRuleRepository

