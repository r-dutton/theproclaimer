[web] PUT /api/internal/intercom/entity-create  (Dataverse.Api.Controllers.Internal.Integration.IntercomController.CreateEntityInIntercom)  [L55–L67] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateIntercomEntityCommand -> CreateIntercomEntityCommandHandler [L60]
    └─ handled_by Dataverse.ApplicationService.Commands.Intercom.CreateIntercomEntityCommandHandler.Handle [L41–L117]
      └─ calls TenantRepository.ReadTable [L109]
      └─ calls UserRepository.ReadQuery [L67]
      └─ maps_to TenantLightDto [L109]
      └─ maps_to UserWithLicensesDto [L67]
        └─ automapper.registration DataverseMappingProfile (User->UserWithLicensesDto) [L90]
      └─ uses_service IntercomService
        └─ method GetContactByExternalId [L79]
          └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.GetContactByExternalId [L17-L124]
            └─ uses_client IntercomClient [L31]
            └─ uses_service IntercomClient
              └─ method GetContacts [L31]
                └─ implementation Dataverse.Services.ModuleIntegrations.Clients.IntercomClient.GetContacts [L25-L174]
      └─ uses_service ITenantIdentificationService (AddScoped)
        └─ method AssignActiveTenant [L65]
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
      └─ CreateIntercomEntityCommand
    └─ handlers 1
      └─ CreateIntercomEntityCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ TenantLightDto
      └─ UserWithLicensesDto

