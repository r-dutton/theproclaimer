[web] GET /api/fyi/documents  (DataGet.Api.Controllers.Integrations.FyiController.GetDocuments)  [L89–L109] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentsQuery [L105]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetDocumentsQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetDocuments [L30]

