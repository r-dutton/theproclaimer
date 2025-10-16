[web] GET /api/ui/fyi-elite/access-info  (Dataverse.Api.Controllers.UI.FyiEliteController.GetAccessInfo)  [L41–L48] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ uses_service IDatagetFyiEliteService (AddTransient)
    └─ method GetAccessInfo [L45]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiEliteService.GetAccessInfo [L13-L53]

