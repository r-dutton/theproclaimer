[web] GET /api/ui/fyi/entities  (Dataverse.Api.Controllers.UI.FyiController.GetEntities)  [L142–L148] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetEntitiesAsync [L145]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetEntitiesAsync [L19-L291]

