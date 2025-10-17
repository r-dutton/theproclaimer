[web] POST /api/internal/teams/  (Dataverse.Api.Controllers.Internal.Core.TeamsController.Create)  [L68–L74] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateTeamCommand -> CreateOrUpdateTeamCommandHandler [L72]
    └─ handled_by Dataverse.ApplicationService.Commands.Firms.CreateOrUpdateTeamCommandHandler.Handle [L38–L78]
      └─ calls TeamRepository (methods: Add,WriteQuery) [L68]
      └─ maps_to TeamDto [L76]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L53]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
            └─ uses_client WorkpapersClient [L78]
            └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
              └─ method GetCurrentSettingsAsync [L52]
                └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.GetCurrentSettingsAsync
            └─ uses_service TenantService
              └─ method GetCurrentSettingsAsync [L44]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentSettingsAsync [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
            └─ uses_cache IDistributedCache.GetRecordAsync [read] [L70]
            └─ uses_cache IDistributedCache.RemoveAsync [write] [L46]
  └─ impact_summary
    └─ clients 1
      └─ WorkpapersClient
    └─ requests 1
      └─ CreateOrUpdateTeamCommand
    └─ handlers 1
      └─ CreateOrUpdateTeamCommandHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ TeamDto

