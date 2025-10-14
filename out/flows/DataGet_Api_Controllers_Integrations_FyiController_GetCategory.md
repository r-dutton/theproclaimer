[web] GET /api/fyi/categories/{categoryId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetCategory)  [L78–L87] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCategoryQuery [L83]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCategoryQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetCategory [L29]

