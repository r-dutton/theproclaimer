[web] POST /api/matching-rules/source/{sourceId}/apply-all  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.ApplyRules)  [L86–L93] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request ApplyMatchingRulesCommand -> ApplyMatchingRulesCommandHandler [L90]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ApplyMatchingRulesCommandHandler.Handle [L96–L646]
      └─ uses_service IAccountTypeProvider
        └─ method GetAccountTypesWithJurisdictionAsync [L238]
          └─ ... (no implementation details available)
      └─ uses_service IBenchmarkCodeSetProvider
        └─ method GetBenchmarkCodeSetAsync [L225]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method WriteQuery [L190]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.WriteQuery
      └─ uses_service IControlledRepository<File> (Scoped (inferred))
        └─ method ReadQuery [L173]
          └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
      └─ uses_service IMatchingRuleProvider
        └─ method GetRulesForFileAsync [L146]
          └─ implementation Workpapers.Next.ApplicationService.Features.MatchingRuleCache.MatchingRuleService.GetRulesForFileAsync [L14-L87]
            └─ calls MatchingRuleRepository.ReadQuery [L83]
            └─ calls MatchingRuleSetRepository.ReadQuery [L67]
            └─ calls MatchingRuleRepository.ReadQuery [L42]
            └─ uses_service MatchingRule
              └─ method GetMatchingRule [L80]
                └─ implementation Workpapers.Next.DomainModel.Model.Workpapers.MatchingRule.GetMatchingRule [L18-L250]
            └─ uses_service Guid?
              └─ method GetDefaultMatchingRuleSet [L75]
                └─ ... (no implementation details available)
            └─ uses_service IControlledRepository<Firm> (Scoped (inferred))
              └─ method GetDefaultMatchingRuleSet [L62]
                └─ implementation Workpapers.Next.Data.Repository.Firms.FirmRepository.GetDefaultMatchingRuleSet
            └─ uses_service TenantService
              └─ method GetDefaultMatchingRuleSet [L55]
                └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetDefaultMatchingRuleSet [L5-L22]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
                        └─ uses_service RequestProcessor
                          └─ method GetCatalogByFirmId [L104]
                            └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                            └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                            └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                            └─ +1 additional_requests elided
                        └─ uses_service FirmLightDto
                          └─ method AssignActiveTenant [L77]
                            └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
  └─ impact_summary
    └─ requests 1
      └─ ApplyMatchingRulesCommand
    └─ handlers 1
      └─ ApplyMatchingRulesCommandHandler
    └─ caches 1
      └─ IMemoryCache

