[web] GET /api/ui/fyi/access-info  (Dataverse.Api.Controllers.UI.FyiController.GetAccessInfo)  [L182–L189] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetAccessInfo [L186]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetAccessInfo [L19-L291]

