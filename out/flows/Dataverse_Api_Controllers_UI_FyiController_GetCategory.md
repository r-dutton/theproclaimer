[web] GET /api/ui/fyi/categories/{categoryId:long}  (Dataverse.Api.Controllers.UI.FyiController.GetCategory)  [L71–L77] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetCategoryAsync [L74]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetCategoryAsync [L19-L291]

