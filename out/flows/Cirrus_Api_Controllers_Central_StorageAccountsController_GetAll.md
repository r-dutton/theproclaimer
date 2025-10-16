[web] GET /api/central/storage-accounts/  (Cirrus.Api.Controllers.Central.StorageAccountsController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to StorageAccountDto [L31]
  └─ calls CentralRepository.ReadTable [L31]
  └─ uses_service CentralRepository
    └─ method ReadTable [L31]
      └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]

