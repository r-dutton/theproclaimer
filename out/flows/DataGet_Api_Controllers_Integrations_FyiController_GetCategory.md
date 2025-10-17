[web] GET /api/fyi/categories/{categoryId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetCategory)  [L78–L87] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCategoryQuery -> GetCategoryQueryHandler [L83]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCategoryQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetCategory [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCategory [L30-L166]
            └─ uses_client FyiHttpClient [L50]
            └─ uses_service FyiHttpClient
              └─ method GetCabinets [L50]
                └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
  └─ impact_summary
    └─ clients 1
      └─ FyiHttpClient
    └─ requests 1
      └─ GetCategoryQuery
    └─ handlers 1
      └─ GetCategoryQueryHandler

