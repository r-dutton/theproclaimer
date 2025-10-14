[web] GET /api/central/databases/{databaseId}  (Cirrus.Api.Controllers.Central.DatabaseController.Get)  [L38–L45] [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to DatabaseDto [L41]
  └─ calls CentralRepository.ReadTable [L41]
  └─ uses_service CentralRepository
    └─ method ReadTable [L41]

