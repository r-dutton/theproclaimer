[web] GET /api/fyi/cabinets/{cabinetId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetCabinet)  [L56–L65] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCabinetQuery -> GetCabinetQueryHandler [L61]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCabinetQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetCabinet [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCabinet [L30-L166]
            └─ uses_client FyiHttpClient [L50]
            └─ uses_service FyiHttpClient
              └─ method GetCabinets [L50]
                └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
  └─ impact_summary
    └─ clients 1
      └─ FyiHttpClient
    └─ requests 1
      └─ GetCabinetQuery
    └─ handlers 1
      └─ GetCabinetQueryHandler

