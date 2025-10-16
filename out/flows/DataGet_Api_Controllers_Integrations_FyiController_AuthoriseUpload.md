[web] GET /api/fyi/documents/{documentVersionId:guid}/authorise-upload  (DataGet.Api.Controllers.Integrations.FyiController.AuthoriseUpload)  [L144–L152] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentUploadAuthorisationQuery [L149]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetDocumentUploadAuthorisationQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method AuthoriseUpload [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.AuthoriseUpload [L30-L166]

