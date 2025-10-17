[web] GET /api/ui/fyi/groups/{groupId:long}  (Dataverse.Api.Controllers.UI.FyiController.GetGroup)  [L166–L172] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetGroupAsync [L169]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetGroupAsync [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

