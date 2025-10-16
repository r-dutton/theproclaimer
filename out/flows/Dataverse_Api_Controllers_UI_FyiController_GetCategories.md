[web] GET /api/ui/fyi/categories  (Dataverse.Api.Controllers.UI.FyiController.GetCategories)  [L63–L69] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetCategoriesAsync [L66]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetCategoriesAsync [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

