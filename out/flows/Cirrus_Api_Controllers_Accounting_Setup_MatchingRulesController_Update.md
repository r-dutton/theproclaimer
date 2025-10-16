[web] PUT /api/accounting/matching-rules/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Update)  [L161–L168] status=200 [auth=user]
  └─ calls MatchingRuleRepository.WriteQuery [L164]
  └─ write MatchingRule [L164]
    └─ reads_from MatchingRules
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ MatchingRule writes=1 reads=0

