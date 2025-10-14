[web] GET /api/fyi/cabinets/{cabinetId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetCabinet)  [L56–L65] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCabinetQuery [L61]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCabinetQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetCabinet [L29]

