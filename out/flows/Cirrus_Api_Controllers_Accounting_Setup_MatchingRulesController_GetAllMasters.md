[web] GET /api/accounting/matching-rules/rule-set/{ruleSetId}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.GetAllMasters)  [L87–L99] status=200 [auth=user,admin]
  └─ maps_to MatchingRuleDto [L90]
    └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
  └─ calls MatchingRuleRepository.ReadQuery [L90]
  └─ query MatchingRule [L90]
    └─ reads_from MatchingRules
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatchingRule writes=0 reads=1
    └─ mappings 1
      └─ MatchingRuleDto

