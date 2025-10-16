[web] GET /api/ui/fyi/documents/{documentVersionId:guid}/downloadUrl  (Dataverse.Api.Controllers.UI.FyiController.GetDocumentDownloadUrl)  [L106–L112] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetDocumentDownloadUrlAsync [L109]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetDocumentDownloadUrlAsync [L19-L291]

