[web] GET /api/central/firms/{dataverseId}  (Cirrus.Api.Controllers.Central.FirmController.Get)  [L63–L72] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to FirmDto [L66]
    └─ converts_to FirmWithStatsDto [L162]
  └─ calls CentralRepository.ReadTable [L66]
  └─ uses_service CentralRepository
    └─ method ReadTable [L66]
      └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]
  └─ impact_summary
    └─ services 1
      └─ CentralRepository
    └─ mappings 1
      └─ FirmDto

