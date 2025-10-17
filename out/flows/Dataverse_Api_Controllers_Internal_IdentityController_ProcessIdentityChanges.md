[web] PUT /api/internal/identity  (Dataverse.Api.Controllers.Internal.IdentityController.ProcessIdentityChanges)  [L46–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to CentralUserDto [L49]
    └─ automapper.registration IdentityMappingProfile (IdentityAccount->CentralUserDto) [L44]
  └─ sends_request UpdateCentralUser -> UpdateCentralUserHandler [L49]
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
    └─ requests 1
      └─ UpdateCentralUser
    └─ handlers 1
      └─ UpdateCentralUserHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ CentralUserDto

