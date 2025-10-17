[web] GET /api/accounting/matching-rules/{id}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Get)  [L61–L78] status=200 [auth=user]
  └─ maps_to MatchingRuleDto [L64]
    └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
  └─ calls MatchingRuleRepository.ReadQuery [L64]
  └─ query MatchingRule [L64]
    └─ reads_from MatchingRules
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatchingRule writes=0 reads=1
    └─ mappings 1
      └─ MatchingRuleDto

