[web] GET /api/ui/fyi/documents/{documentVersionId:guid}/authorise-upload  (Dataverse.Api.Controllers.UI.FyiController.AuthoriseUpload)  [L114–L120] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method AuthoriseUploadAsync [L117]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.AuthoriseUploadAsync [L19-L291]
  └─ impact_summary
    └─ services 1
      └─ IDatagetFyiService

