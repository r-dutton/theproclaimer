[web] PUT /api/internal/intercom/sync-all  (Dataverse.Api.Controllers.Internal.Integration.IntercomController.SyncTenantDataToIntercom)  [L27–L39] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request SyncTenantDataToIntercomCommand -> SyncTenantDataToIntercomCommandHandler [L32]
    └─ handled_by Dataverse.ApplicationService.Commands.Intercom.SyncTenantDataToIntercomCommandHandler.Handle [L38–L161]
      └─ calls UserRepository.ReadQuery [L93]
      └─ calls TenantRepository.ReadTable [L74]
      └─ maps_to UserWithLicensesDto [L93]
        └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L90]
      └─ maps_to TenantLightDto [L74]
      └─ uses_service AsyncRetryPolicy
        └─ method ExecuteAsync [L124]
          └─ ... (no implementation details available)
      └─ uses_service IntercomService
        └─ method CreateOrUpdateCompany [L88]
          └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.CreateOrUpdateCompany [L17-L124]
            └─ uses_client IntercomClient [L31]
            └─ uses_service IntercomClient
              └─ method GetContacts [L31]
                └─ implementation Dataverse.Services.ModuleIntegrations.Clients.IntercomClient.GetContacts [L25-L174]
      └─ uses_service ITenantIdentificationService (AddScoped)
        └─ method AssignActiveTenant [L72]
          └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.AssignActiveTenant [L27-L149]
            └─ uses_service TenantDetails
              └─ method AssignActiveTenant [L77]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
            └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ clients 1
      └─ IntercomClient
    └─ requests 1
      └─ SyncTenantDataToIntercomCommand
    └─ handlers 1
      └─ SyncTenantDataToIntercomCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ TenantLightDto
      └─ UserWithLicensesDto

