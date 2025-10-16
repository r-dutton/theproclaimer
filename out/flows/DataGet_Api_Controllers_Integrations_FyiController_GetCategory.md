[web] GET /api/fyi/categories/{categoryId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetCategory)  [L78–L87] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCategoryQuery [L83]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCategoryQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetCategory [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCategory [L30-L166]

