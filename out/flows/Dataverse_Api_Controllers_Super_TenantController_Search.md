[web] GET /api/super/tenants/  (Dataverse.Api.Controllers.Super.TenantController.Search)  [L74–L88] status=200 [auth=Authentication.MasterPolicy]
  └─ sends_request FindTenantsQuery [L86]
    └─ handled_by Cirrus.Central.Dataverse.Queries.FindTenantsQueryHandler.Handle [L32–L52]
      └─ uses_service DataverseService
        └─ method Get [L50]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]

