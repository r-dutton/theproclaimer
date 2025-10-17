[web] GET /api/ui/fyi/documents/{documentId:guid}  (Dataverse.Api.Controllers.UI.FyiController.GetDocument)  [L98–L104] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetDocumentAsync [L101]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentAsync [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

