[web] POST /api/ui/users/{userId:guid}/resend-invite  (Dataverse.Api.Controllers.UI.Core.UsersController.ResendInvite)  [L307–L324] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository (methods: WriteQuery,ReadQuery) [L318]
  └─ write User [L318]
  └─ query User [L311]
  └─ uses_service UserRepository
    └─ method ReadQuery [L311]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ sends_request ResendInviteCommand -> ResendInviteCommandHandler [L316]
    └─ handled_by Cirrus.Central.Commands.ResendInviteCommandHandler.Handle [L33–L74]
      └─ calls CentralRepository (methods: Commit,WriteTable) [L68]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L64]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service DataverseService
        └─ method Post [L59]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Post [L14-L66]
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
    └─ entities 1 (writes=1, reads=1)
      └─ User writes=1 reads=1
    └─ services 1
      └─ UserRepository
    └─ requests 1
      └─ ResendInviteCommand
    └─ handlers 1
      └─ ResendInviteCommandHandler
    └─ caches 1
      └─ IMemoryCache

