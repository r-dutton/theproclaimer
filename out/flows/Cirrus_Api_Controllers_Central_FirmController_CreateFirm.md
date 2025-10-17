[web] POST /api/central/firms/  (Cirrus.Api.Controllers.Central.FirmController.CreateFirm)  [L85–L92] status=201 [auth=Authentication.MachineToMachinePolicy]
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
  └─ impact_summary
    └─ requests 1
      └─ CreateFirmCommand
    └─ handlers 1
      └─ CreateFirmCommandHandler
    └─ caches 1
      └─ IMemoryCache

