[web] PUT /api/super/cirrus/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.UpdateSubscription)  [L104–L120] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_client CirrusClient [L117]
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
  └─ calls TenantRepository.WriteTable [L112]
  └─ write Tenant [L112]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L112]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.WriteTable [L15-L180]
  └─ sends_request CreateOrUpdateSubscriptionCommand -> CreateOrUpdateSubscriptionCommandHandler [L119]
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore> (Scoped (inferred))
        └─ method ReadQuery [L79]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentStoreRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ remote_endpoints 3 (calls=3) scopes=service:3
      └─ GET /api/central/databases -> CirrusClient via CirrusClient [query] (x1)
      └─ POST /api/central/firms -> CirrusClient via CirrusClient [command] (x1)
      └─ GET /api/central/storage-accounts -> CirrusClient via CirrusClient [query] (x1)
    └─ entities 1 (writes=1, reads=0)
      └─ Tenant writes=1 reads=0
    └─ clients 1
      └─ CirrusClient
    └─ services 2
      └─ CentralRepository
      └─ TenantRepository
    └─ requests 2
      └─ CreateFirmCommand
      └─ CreateOrUpdateSubscriptionCommand
    └─ handlers 2
      └─ CreateFirmCommandHandler
      └─ CreateOrUpdateSubscriptionCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ DatabaseLightDto
      └─ StorageAccountDto

