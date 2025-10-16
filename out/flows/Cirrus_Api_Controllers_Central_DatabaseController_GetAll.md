[web] GET /api/central/databases/  (Cirrus.Api.Controllers.Central.DatabaseController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to DatabaseLightDto [L31]
  └─ calls CentralRepository.ReadTable [L31]
  └─ uses_service CentralRepository
    └─ method ReadTable [L31]
      └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]
  └─ impact_summary
    └─ services 1
      └─ CentralRepository
    └─ mappings 1
      └─ DatabaseLightDto

