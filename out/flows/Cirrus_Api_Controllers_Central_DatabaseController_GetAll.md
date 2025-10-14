[web] GET /api/central/databases/  (Cirrus.Api.Controllers.Central.DatabaseController.GetAll)  [L28–L36] [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to DatabaseLightDto [L31]
  └─ calls CentralRepository.ReadTable [L31]
  └─ uses_service CentralRepository
    └─ method ReadTable [L31]

