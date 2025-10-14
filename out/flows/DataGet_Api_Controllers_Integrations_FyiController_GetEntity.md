[web] GET /api/fyi/entities/{entityId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetEntity)  [L186–L195] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetEntityQuery [L191]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetEntityQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetEntity [L29]

