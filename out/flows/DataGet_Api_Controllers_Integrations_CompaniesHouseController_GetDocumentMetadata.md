[web] GET /api/companies-house/document/{documentId}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetDocumentMetadata)  [L398–L406] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentMetadataQuery [L403]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetDocumentMetadataQueryHandler.Handle [L15–L24]

