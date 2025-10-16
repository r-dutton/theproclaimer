[web] GET /api/central/databases/{databaseId}  (Cirrus.Api.Controllers.Central.DatabaseController.Get)  [L38–L45] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to DatabaseDto [L41]
  └─ calls CentralRepository.ReadTable [L41]
  └─ uses_service CentralRepository
    └─ method ReadTable [L41]
      └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]
  └─ impact_summary
    └─ services 1
      └─ CentralRepository
    └─ mappings 1
      └─ DatabaseDto

