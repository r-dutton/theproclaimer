[web] PUT /api/ui/fyi/access-info  (Dataverse.Api.Controllers.UI.FyiController.Authenticate)  [L191–L196] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method Authenticate [L195]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.Authenticate [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

