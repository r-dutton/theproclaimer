[web] GET /api/accounting/matching-rules/{id}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Get)  [L61–L78] [auth=user]
  └─ maps_to MatchingRuleDto [L64]
    └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRule->MatchingRuleDto) [L887]
  └─ calls MatchingRuleRepository.ReadQuery [L64]
  └─ queries MatchingRule [L64]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method ReadQuery [L64]

