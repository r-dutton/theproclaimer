[web] GET /api/accounting/matching-rules/file/{fileId}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.GetAll)  [L108–L116] status=200 [auth=user]
  └─ sends_request GetMatchingRulesForFileQuery [L113]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetMatchingRulesForFileHandler.Handle [L35–L132]
      └─ maps_to AccountUltraLightDto [L108]
        └─ automapper.registration CirrusMappingProfile (Account->AccountUltraLightDto) [L312]
      └─ maps_to AccountTypeLightDto [L98]
        └─ automapper.registration CirrusMappingProfile (AccountType->AccountTypeLightDto) [L246]
        └─ automapper.registration WorkpapersMappingProfile (AccountType->AccountTypeLightDto) [L707]
      └─ maps_to MatchingRuleDto [L77]
        └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
        └─ automapper.registration WorkpapersMappingProfile (MatchingRule->MatchingRuleDto) [L887]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L108]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<AccountType>
        └─ method ReadQuery [L98]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L70]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<MatchingRule>
        └─ method ReadQuery [L77]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L79]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L68]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

