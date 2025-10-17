[web] DELETE /api/ui/fyi-elite/disconnect  (Dataverse.Api.Controllers.UI.FyiEliteController.Disconnect)  [L57–L62] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ uses_service IDatagetFyiEliteService (AddTransient)
    └─ method Disconnect [L61]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiEliteService.Disconnect [L13-L53]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiEliteService

