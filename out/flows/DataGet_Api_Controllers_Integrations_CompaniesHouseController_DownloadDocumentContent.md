[web] GET /api/companies-house/document/{documentId}/download  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.DownloadDocumentContent)  [L418–L430] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentContentQuery -> GetDocumentContentQueryHandler [L423]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetDocumentContentQueryHandler.Handle [L15–L31]
  └─ impact_summary
    └─ requests 1
      └─ GetDocumentContentQuery
    └─ handlers 1
      └─ GetDocumentContentQueryHandler

