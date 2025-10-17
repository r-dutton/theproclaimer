[web] GET /api/ui/fyi/verify-connection  (Dataverse.Api.Controllers.UI.FyiController.VerifyConnection)  [L214–L221] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method VerifyConnection [L218]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.VerifyConnection [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

