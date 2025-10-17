[web] GET /api/ui/fyi/users  (Dataverse.Api.Controllers.UI.FyiController.GetUsers)  [L174–L180] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetUsersAsync [L177]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetUsersAsync [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

