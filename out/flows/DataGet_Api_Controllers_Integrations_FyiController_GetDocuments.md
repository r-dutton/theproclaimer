[web] GET /api/fyi/documents  (DataGet.Api.Controllers.Integrations.FyiController.GetDocuments)  [L89–L109] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDocumentsQuery [L105]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetDocumentsQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetDocuments [L30]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetDocuments [L30-L166]

