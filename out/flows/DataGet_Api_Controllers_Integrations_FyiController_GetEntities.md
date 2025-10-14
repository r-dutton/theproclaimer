[web] GET /api/fyi/entities  (DataGet.Api.Controllers.Integrations.FyiController.GetEntities)  [L175–L184] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetEntitiesQuery [L180]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetEntitiesQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetEntities [L30]

