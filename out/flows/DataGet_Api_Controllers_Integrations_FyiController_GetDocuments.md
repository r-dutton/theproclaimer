[web] GET /api/fyi/documents  (DataGet.Api.Controllers.Integrations.FyiController.GetDocuments)  [L89–L109] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentsQuery -> GetDocumentsQueryHandler [L105]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetDocumentsQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetDocuments [L30]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetDocuments [L30-L166]
            └─ uses_client FyiHttpClient [L50]
            └─ uses_service FyiHttpClient
              └─ method GetCabinets [L50]
                └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
  └─ impact_summary
    └─ clients 1
      └─ FyiHttpClient
    └─ requests 1
      └─ GetDocumentsQuery
    └─ handlers 1
      └─ GetDocumentsQueryHandler

