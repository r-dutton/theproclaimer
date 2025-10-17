[web] GET /api/ui/fyi/documents  (Dataverse.Api.Controllers.UI.FyiController.GetDocuments)  [L79–L96] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetDocumentsAsync [L93]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentsAsync [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

