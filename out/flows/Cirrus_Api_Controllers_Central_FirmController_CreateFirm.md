[web] POST /api/central/firms/  (Cirrus.Api.Controllers.Central.FirmController.CreateFirm)  [L85–L92] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request CreateFirmCommand [L88]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Commands.CreateFirmCommandHandler.Handle [L37–L162]
      └─ calls CentralRepository.Commit [L76]
      └─ calls CentralRepository.Add [L75]
      └─ calls CentralRepository.WriteTable [L61]
      └─ calls CentralRepository.WriteTable [L58]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L86]
      └─ uses_service TenantService
        └─ method AssignCurrentTenantId [L78]

