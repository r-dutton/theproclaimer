[web] PUT /api/internal/intercom/sync-entity  (Dataverse.Api.Controllers.Internal.Integration.IntercomController.SyncEntityDataToIntercom)  [L41–L53] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request UpdateEntityDataToIntercomCommand -> UpdateEntityDataToIntercomCommandHandler [L46]
    └─ handled_by Dataverse.ApplicationService.Commands.Intercom.UpdateEntityDataToIntercomCommandHandler.Handle [L40–L113]
      └─ calls TenantRepository.ReadTable [L105]
      └─ calls UserRepository.ReadQuery [L81]
      └─ maps_to TenantLightDto [L105]
      └─ maps_to UserWithLicensesDto [L81]
        └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L90]
      └─ uses_service IntercomService
        └─ method GetContactByExternalId [L89]
          └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.GetContactByExternalId [L17-L124]
            └─ uses_client IntercomClient [L31]
            └─ uses_service IntercomClient
              └─ method GetContacts [L31]
                └─ implementation Dataverse.Services.ModuleIntegrations.Clients.IntercomClient.GetContacts [L25-L174]
      └─ uses_service ITenantIdentificationService (AddScoped)
        └─ method AssignActiveTenant [L64]
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
      └─ UpdateEntityDataToIntercomCommand
    └─ handlers 1
      └─ UpdateEntityDataToIntercomCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ TenantLightDto
      └─ UserWithLicensesDto

