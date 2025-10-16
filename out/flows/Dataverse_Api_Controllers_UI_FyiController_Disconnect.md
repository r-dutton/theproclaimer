[web] DELETE /api/ui/fyi/disconnect  (Dataverse.Api.Controllers.UI.FyiController.Disconnect)  [L198–L203] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method Disconnect [L202]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.Disconnect [L19-L291]

