[web] GET /api/internal/tenants/  (Dataverse.Api.Controllers.Internal.TenantController.Search)  [L42–L51] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request FindTenantsQuery [L50]
    └─ handled_by Cirrus.Central.Dataverse.Queries.FindTenantsQueryHandler.Handle [L32–L52]
      └─ uses_service DataverseService
        └─ method Get [L50]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]

