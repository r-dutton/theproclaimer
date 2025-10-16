[web] GET /api/ui/fyi/groups  (Dataverse.Api.Controllers.UI.FyiController.GetGroups)  [L158–L164] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetGroupsAsync [L161]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetGroupsAsync [L19-L291]

