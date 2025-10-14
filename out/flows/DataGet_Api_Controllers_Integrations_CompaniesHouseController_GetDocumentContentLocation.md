[web] GET /api/companies-house/document/{documentId}/content  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetDocumentContentLocation)  [L408–L416] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentContentLocationQuery [L413]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetDocumentContentLocationQueryHandler.Handle [L18–L31]

