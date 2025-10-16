[web] GET /api/super/cirrus/databases  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.GetAllDatabases)  [L122–L126] status=200 [auth=Authentication.MasterPolicy]
  └─ uses_client CirrusClient [L125]
    └─ calls GetAll (GET /api/central/databases) [L125]
      └─ remote_endpoint_lookup route=/api/central/databases verb=GET
        └─ [web] GET /api/central/databases/  (Cirrus.Api.Controllers.Central.DatabaseController.GetAll)  [L28–L36] status=200 [auth=Authentication.MachineToMachinePolicy]
          └─ maps_to DatabaseLightDto [L31]
          └─ calls CentralRepository.ReadTable [L31]
          └─ uses_service CentralRepository
            └─ method ReadTable [L31]
              └─ implementation Cirrus.Central.Data.CentralRepository.ReadTable [L10-L55]
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/central/databases -> CirrusClient via CirrusClient [query] (x1)
    └─ clients 1
      └─ CirrusClient
    └─ services 1
      └─ CentralRepository
    └─ mappings 1
      └─ DatabaseLightDto

