[web] PUT /api/internal/contacts  (Dataverse.Api.Controllers.Internal.Core.ContactsController.UpdateDetails)  [L63–L69] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateContactCommand -> CreateOrUpdateContactCommandHandler [L66]
    └─ handled_by Dataverse.ApplicationService.Commands.Clients.CreateOrUpdateContactCommandHandler.Handle [L39–L79]
      └─ maps_to ContactDto [L77]
      └─ uses_service IControlledRepository<Contact> (Scoped (inferred))
        └─ method WriteQuery [L61]
          └─ implementation Dataverse.Data.Repository.Clients.ContactRepository.WriteQuery
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L54]
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
      └─ CreateOrUpdateContactCommand
    └─ handlers 1
      └─ CreateOrUpdateContactCommandHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ ContactDto

