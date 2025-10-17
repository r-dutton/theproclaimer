[web] PUT /api/dataverse/teams  (Cirrus.Api.Controllers.Dataverse.DataverseController.UpdateTeamChanges)  [L58–L63] status=200 [auth=Authentication.RequireTenantIdPolicy]
  └─ uses_service ITenantService (AddScoped)
    └─ method GetCurrentTenant [L61]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ sends_request ProcessDataverseTeamCommand -> ProcessDataverseTeamCommandHandler [L62]
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseTeamCommandHandler.Handle [L23–L64]
      └─ uses_service IControlledRepository<Team> (Scoped (inferred))
        └─ method WriteQuery [L37]
          └─ implementation Cirrus.Data.Repository.Firm.TeamRepository.WriteQuery
  └─ impact_summary
    └─ services 1
      └─ ITenantService
    └─ requests 1
      └─ ProcessDataverseTeamCommand
    └─ handlers 1
      └─ ProcessDataverseTeamCommandHandler
    └─ caches 1
      └─ IMemoryCache

