[web] GET /api/central/firms/{dataverseId}  (Cirrus.Api.Controllers.Central.FirmController.Get)  [L63–L72] [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to FirmDto [L66]
    └─ converts_to FirmWithStatsDto [L162]
  └─ calls CentralRepository.ReadTable [L66]
  └─ uses_service CentralRepository
    └─ method ReadTable [L66]

