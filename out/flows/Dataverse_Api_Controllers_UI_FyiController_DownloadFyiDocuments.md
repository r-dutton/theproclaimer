[web] POST /api/ui/fyi/download-documents  (Dataverse.Api.Controllers.UI.FyiController.DownloadFyiDocuments)  [L223–L244] status=201 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetDocumentDownloadUrlAsync [L232]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentDownloadUrlAsync [L19-L291]

