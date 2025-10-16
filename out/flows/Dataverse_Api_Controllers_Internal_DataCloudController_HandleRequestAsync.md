[web] GET /api/internal/data-cloud  (Dataverse.Api.Controllers.Internal.DataCloudController.HandleRequestAsync)  [L92–L104] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service UnitOfWork
    └─ method Table [L98]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

