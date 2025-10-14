[web] GET /api/fyi/categories  (DataGet.Api.Controllers.Integrations.FyiController.GetCategories)  [L67–L76] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCategoriesQuery [L72]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCategoriesQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetCategories [L30]

