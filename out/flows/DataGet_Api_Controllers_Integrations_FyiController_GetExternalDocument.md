[web] GET /api/fyi/documents/external  (DataGet.Api.Controllers.Integrations.FyiController.GetExternalDocument)  [L122–L131] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetExternalDocumentQuery -> GetExternalDocumentQueryHandler [L127]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetExternalDocumentQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetExternalDocument [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetExternalDocument [L30-L166]
            └─ uses_client FyiHttpClient [L50]
            └─ uses_service FyiHttpClient
              └─ method GetCabinets [L50]
                └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
  └─ impact_summary
    └─ clients 1
      └─ FyiHttpClient
    └─ requests 1
      └─ GetExternalDocumentQuery
    └─ handlers 1
      └─ GetExternalDocumentQueryHandler

