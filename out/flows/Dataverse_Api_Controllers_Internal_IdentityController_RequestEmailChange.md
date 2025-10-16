[web] PUT /api/internal/identity/request-email-change  (Dataverse.Api.Controllers.Internal.IdentityController.RequestEmailChange)  [L68–L93] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to CentralUserDto (var dto) [L86]
  └─ calls TenantRepository.ReadTable [L83]
  └─ query Tenant [L83]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L83]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ sends_request UpdateCentralUser -> UpdateCentralUserHandler [L90]
    └─ handled_by Cirrus.Central.Commands.UpdateCentralUserHandler.Handle [L37–L93]
      └─ calls CentralRepository (methods: Commit,WriteTable,ReadTable) [L77]
      └─ uses_service DataverseService
        └─ method GetStandardInviteInfo [L86]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.GetStandardInviteInfo [L14-L66]
            └─ uses_service TenantService
              └─ method GetStandardQueryParams [L62]
                └─ implementation Cirrus.Central.Tenants.TenantService.GetStandardQueryParams [L26-L139]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method GetCatalogByDataverseId [L111]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
                  └─ uses_service Tenant
                    └─ method AssignCurrentTenantId [L80]
                      └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 1
      └─ TenantRepository
    └─ requests 1
      └─ UpdateCentralUser
    └─ handlers 1
      └─ UpdateCentralUserHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ CentralUserDto

