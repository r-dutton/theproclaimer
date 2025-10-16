[web] GET /api/fyi/entities  (DataGet.Api.Controllers.Integrations.FyiController.GetEntities)  [L175–L184] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetEntitiesQuery -> GetEntitiesQueryHandler [L180]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetEntitiesQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetEntities [L30]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetEntities [L30-L166]
            └─ uses_client FyiHttpClient [L50]
            └─ uses_service FyiHttpClient
              └─ method GetCabinets [L50]
                └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
  └─ impact_summary
    └─ clients 1
      └─ FyiHttpClient
    └─ requests 1
      └─ GetEntitiesQuery
    └─ handlers 1
      └─ GetEntitiesQueryHandler

