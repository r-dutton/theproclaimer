[web] GET /api/fyi/categories  (DataGet.Api.Controllers.Integrations.FyiController.GetCategories)  [L67–L76] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCategoriesQuery [L72]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCategoriesQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetCategories [L30]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCategories [L30-L166]

