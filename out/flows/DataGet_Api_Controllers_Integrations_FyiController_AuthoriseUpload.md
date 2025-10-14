[web] GET /api/fyi/documents/{documentVersionId:guid}/authorise-upload  (DataGet.Api.Controllers.Integrations.FyiController.AuthoriseUpload)  [L144–L152] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentUploadAuthorisationQuery [L149]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetDocumentUploadAuthorisationQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method AuthoriseUpload [L29]

