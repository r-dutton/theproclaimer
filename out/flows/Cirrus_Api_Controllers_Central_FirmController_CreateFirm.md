[web] POST /api/central/firms/  (Cirrus.Api.Controllers.Central.FirmController.CreateFirm)  [L85–L92] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request CreateFirmCommand [L88]
    └─ handled_by Cirrus.Central.Commands.CreateFirmCommandHandler.Handle [L37–L162]
      └─ calls CentralRepository.Commit [L76]
      └─ calls CentralRepository.Add [L75]
      └─ calls CentralRepository.WriteTable [L61]
      └─ calls CentralRepository.WriteTable [L58]
      └─ uses_service TenantService
        └─ method AssignCurrentTenantId [L78]
          └─ implementation Cirrus.Central.Tenants.TenantService.AssignCurrentTenantId [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.AssignCurrentTenantId [L26-L139]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L86]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

