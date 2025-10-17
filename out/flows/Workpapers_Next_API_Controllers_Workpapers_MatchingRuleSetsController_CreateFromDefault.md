[web] POST /api/matching-rule-sets/default/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.CreateFromDefault)  [L68–L74] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request CreateRuleSetFromMasterCommand -> CreateRuleSetFromMasterCommandHandler [L71]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.MatchingRuleSets.CreateRuleSetFromMasterCommandHandler.Handle [L26–L124]
      └─ calls StandardAccountRepository.ReadQuery [L98]
      └─ calls MatchingRuleRepository (methods: Add,ReadQuery) [L84]
      └─ calls MatchingRuleSetRepository (methods: Add,ReadQuery) [L66]
      └─ uses_service IControlledRepository<Firm> (Scoped (inferred))
        └─ method ReadQuery [L69]
          └─ implementation Workpapers.Next.Data.Repository.Firms.FirmRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L68]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
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
      └─ CreateRuleSetFromMasterCommand
    └─ handlers 1
      └─ CreateRuleSetFromMasterCommandHandler
    └─ caches 1
      └─ IMemoryCache

