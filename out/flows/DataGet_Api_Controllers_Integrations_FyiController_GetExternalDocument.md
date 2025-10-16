[web] GET /api/fyi/documents/external  (DataGet.Api.Controllers.Integrations.FyiController.GetExternalDocument)  [L122–L131] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetExternalDocumentQuery [L127]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetExternalDocumentQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetExternalDocument [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetExternalDocument [L30-L166]

