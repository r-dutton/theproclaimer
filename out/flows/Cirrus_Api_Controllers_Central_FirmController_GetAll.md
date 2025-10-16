[web] GET /api/central/firms/  (Cirrus.Api.Controllers.Central.FirmController.GetAll)  [L41–L49] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to FirmDto [L44]
    └─ converts_to FirmWithStatsDto [L162]
  └─ calls CentralRepository.ReadTable [L44]
  └─ uses_service CentralRepository
    └─ method ReadTable [L44]
      └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]

