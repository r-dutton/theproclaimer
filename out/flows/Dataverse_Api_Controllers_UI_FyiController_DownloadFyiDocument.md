[web] POST /api/ui/fyi/download-document  (Dataverse.Api.Controllers.UI.FyiController.DownloadFyiDocument)  [L246–L263] status=201 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetDocumentDownloadUrlAsync [L251]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentDownloadUrlAsync [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

