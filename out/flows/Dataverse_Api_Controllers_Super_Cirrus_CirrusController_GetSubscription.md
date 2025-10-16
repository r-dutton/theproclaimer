[web] GET /api/super/cirrus/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.GetSubscription)  [L44–L62] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SubscriptionDto [L48]
  └─ uses_client CirrusClient [L60]
    └─ calls GetAll (GET /api/central/storage-accounts) [L131]
      └─ remote_endpoint_lookup route=/api/central/storage-accounts verb=GET
        └─ [web] GET /api/central/storage-accounts/  (Cirrus.Api.Controllers.Central.StorageAccountsController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ maps_to StorageAccountDto [L31]
          └─ calls CentralRepository.ReadTable [L31]
          └─ uses_service CentralRepository
            └─ method ReadTable [L31]
              └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]
    └─ calls GetAll (GET /api/central/databases) [L125]
      └─ remote_endpoint_lookup route=/api/central/databases verb=GET
        └─ [web] GET /api/central/databases/  (Cirrus.Api.Controllers.Central.DatabaseController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ maps_to DatabaseLightDto [L31]
          └─ calls CentralRepository.ReadTable [L31]
          └─ uses_service CentralRepository
            └─ method ReadTable [L31]
              └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable (see previous expansion)
    └─ calls CreateFirm (POST /api/central/firms) [L96]
      └─ remote_endpoint_lookup route=/api/central/firms verb=POST
        └─ [web] POST /api/central/firms/  (Cirrus.Api.Controllers.Central.FirmController.CreateFirm)  [L85–L92] status=201 [auth=Authentication.MachineToMachinePolicy]
          └─ sends_request CreateFirmCommand -> CreateFirmCommandHandler [L88]
            └─ handled_by Cirrus.Central.Commands.CreateFirmCommandHandler.Handle [L37–L162]
              └─ calls CentralRepository (methods: Commit,Add,WriteTable) [L76]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method ProcessAsync [L86]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
                    └─ ... (no dispatches detected)
              └─ uses_service TenantService
                └─ method AssignCurrentTenantId [L78]
                  └─ implementation Cirrus.Central.Tenants.TenantService.AssignCurrentTenantId [L26-L139]
                    └─ uses_service Tenant
                      └─ method AssignCurrentTenantId [L80]
                        └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
                    └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
  └─ calls TenantRepository.ReadTable [L48]
  └─ query Tenant [L48]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L48]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ impact_summary
    └─ remote_endpoints 3 (calls=3) scopes=service:3
      └─ GET /api/central/databases -> CirrusClient via CirrusClient [query] (x1)
      └─ POST /api/central/firms -> CirrusClient via CirrusClient [command] (x1)
      └─ GET /api/central/storage-accounts -> CirrusClient via CirrusClient [query] (x1)
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ clients 1
      └─ CirrusClient
    └─ services 2
      └─ CentralRepository
      └─ TenantRepository
    └─ requests 1
      └─ CreateFirmCommand
    └─ handlers 1
      └─ CreateFirmCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 3
      └─ DatabaseLightDto
      └─ StorageAccountDto
      └─ SubscriptionDto

