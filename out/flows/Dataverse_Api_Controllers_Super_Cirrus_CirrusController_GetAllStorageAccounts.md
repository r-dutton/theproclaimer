[web] GET /api/super/cirrus/storage-accounts  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.GetAllStorageAccounts)  [L128–L132] status=200 [auth=Authentication.MasterPolicy]
  └─ uses_client CirrusClient [L131]
    └─ calls GetAll (GET /api/central/storage-accounts) [L131]
      └─ remote_endpoint_lookup route=/api/central/storage-accounts verb=GET
        └─ [web] GET /api/central/storage-accounts/  (Cirrus.Api.Controllers.Central.StorageAccountsController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ maps_to StorageAccountDto [L31]
          └─ calls CentralRepository.ReadTable [L31]
          └─ uses_service CentralRepository
            └─ method ReadTable [L31]
              └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/central/storage-accounts -> CirrusClient via CirrusClient [query] (x1)
    └─ clients 1
      └─ CirrusClient
    └─ services 1
      └─ CentralRepository
    └─ mappings 1
      └─ StorageAccountDto

