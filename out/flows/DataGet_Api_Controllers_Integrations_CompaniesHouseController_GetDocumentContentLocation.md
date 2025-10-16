[web] GET /api/companies-house/document/{documentId}/content  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetDocumentContentLocation)  [L408–L416] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentContentLocationQuery -> GetDocumentContentLocationQueryHandler [L413]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetDocumentContentLocationQueryHandler.Handle [L18–L31]
  └─ impact_summary
    └─ requests 1
      └─ GetDocumentContentLocationQuery
    └─ handlers 1
      └─ GetDocumentContentLocationQueryHandler

