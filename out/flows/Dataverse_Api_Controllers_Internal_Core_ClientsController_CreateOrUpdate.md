[web] POST /api/internal/clients/create-or-update  (Dataverse.Api.Controllers.Internal.Core.ClientsController.CreateOrUpdate)  [L58–L65] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateClientCommand -> CreateOrUpdateClientCommandHandler [L61]
    └─ handled_by Dataverse.ApplicationService.Commands.Clients.CreateOrUpdateClientCommandHandler.Handle [L48–L209]
      └─ calls OfficeRepository.ReadQuery [L193]
      └─ calls ClientRepository.Add [L186]
      └─ calls OfficeRepository.ReadQuery [L173]
      └─ calls UserRepository.ReadQuery [L149]
      └─ calls OfficeRepository.ReadQuery [L137]
      └─ calls ClientRepository (methods: WriteQuery,ReadQuery) [L124]
      └─ calls UserRepository.ReadQuery [L93]
      └─ calls TeamRepository.ReadQuery [L90]
      └─ calls OfficeRepository.ReadQuery [L89]
      └─ maps_to ClientDto [L207]
      └─ uses_client ClientRepository [L95]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L179]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service IControlledRepository<Contact> (Scoped (inferred))
        └─ method ReadQuery [L94]
          └─ implementation Dataverse.Data.Repository.Clients.ContactRepository.ReadQuery
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L81]
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
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
            └─ uses_cache IDistributedCache.GetRecordAsync [read] [L70]
            └─ uses_cache IDistributedCache.RemoveAsync [write] [L46]
  └─ impact_summary
    └─ clients 2
      └─ ClientRepository
      └─ WorkpapersClient
    └─ requests 1
      └─ CreateOrUpdateClientCommand
    └─ handlers 1
      └─ CreateOrUpdateClientCommandHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ ClientDto

