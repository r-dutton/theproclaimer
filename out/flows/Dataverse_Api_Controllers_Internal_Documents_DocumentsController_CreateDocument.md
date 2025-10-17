[web] POST /api/internal/documents/  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateDocument)  [L78–L84] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateDocumentCommand -> CreateDocumentCommandHandler [L81]
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.CreateDocumentCommandHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method CreateDocument [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.CreateDocument [L30-L166]
            └─ uses_client FyiHttpClient [L50]
            └─ uses_service FyiHttpClient
              └─ method GetCabinets [L50]
                └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
  └─ returns FyiDocumentDto [L81]
  └─ impact_summary
    └─ clients 1
      └─ FyiHttpClient
    └─ requests 1
      └─ CreateDocumentCommand
    └─ handlers 1
      └─ CreateDocumentCommandHandler
    └─ mappings 1
      └─ FyiDocumentDto

