[web] GET /api/ui/fyi/entities/{entityId:long}  (Dataverse.Api.Controllers.UI.FyiController.GetEntity)  [L150–L156] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetEntityAsync [L153]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetEntityAsync [L19-L291]

