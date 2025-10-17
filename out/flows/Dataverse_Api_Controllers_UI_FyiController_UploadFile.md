[web] GET /api/ui/fyi  (Dataverse.Api.Controllers.UI.FyiController.UploadFile)  [L302–L307] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method UploadFile [L304]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.UploadFile [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

