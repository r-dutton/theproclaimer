[web] GET /api/fyi/documents/{documentVersionId:guid}/downloadUrl  (DataGet.Api.Controllers.Integrations.FyiController.GetDocumentDownloadUrl)  [L133–L142] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentDownloadUrlQuery [L138]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetDocumentDownloadUrlQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetDocumentDownloadUrl [L29]

