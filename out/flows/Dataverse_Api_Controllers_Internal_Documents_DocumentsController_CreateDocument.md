[web] POST /api/internal/documents/  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateDocument)  [L78–L84] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateDocumentCommand [L81]
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.CreateDocumentCommandHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method CreateDocument [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.CreateDocument [L30-L166]
  └─ returns FyiDocumentDto [L81]

