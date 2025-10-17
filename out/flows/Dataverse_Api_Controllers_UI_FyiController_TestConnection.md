[web] GET /api/ui/fyi/test-connection  (Dataverse.Api.Controllers.UI.FyiController.TestConnection)  [L205–L212] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method TestConnection [L209]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.TestConnection [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

