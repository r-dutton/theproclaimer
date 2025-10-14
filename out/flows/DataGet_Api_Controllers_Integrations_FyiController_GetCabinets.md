[web] GET /api/fyi/cabinets  (DataGet.Api.Controllers.Integrations.FyiController.GetCabinets)  [L45–L54] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCabinetsQuery [L50]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCabinetsQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetCabinets [L30]

