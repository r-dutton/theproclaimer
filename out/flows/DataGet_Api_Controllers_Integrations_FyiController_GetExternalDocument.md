[web] GET /api/fyi/documents/external  (DataGet.Api.Controllers.Integrations.FyiController.GetExternalDocument)  [L122–L131] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetExternalDocumentQuery [L127]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetExternalDocumentQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetExternalDocument [L29]

