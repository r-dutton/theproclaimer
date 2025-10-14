[web] GET /api/central/storage-accounts/  (Cirrus.Api.Controllers.Central.StorageAccountsController.GetAll)  [L28–L36] [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to StorageAccountDto [L31]
  └─ calls CentralRepository.ReadTable [L31]
  └─ uses_service CentralRepository
    └─ method ReadTable [L31]

