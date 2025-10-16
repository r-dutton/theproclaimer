[web] PUT /api/ui/fyi-elite/access-info  (Dataverse.Api.Controllers.UI.FyiEliteController.Authenticate)  [L50–L55] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ uses_service IDatagetFyiEliteService (AddTransient)
    └─ method Authenticate [L54]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiEliteService.Authenticate [L13-L53]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiEliteService

