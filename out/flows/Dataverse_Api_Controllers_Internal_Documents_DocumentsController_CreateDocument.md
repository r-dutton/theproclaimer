[web] POST /api/internal/documents/  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateDocument)  [L78–L84] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateDocumentCommand [L81]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.CreateDocumentCommandHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method CreateDocument [L29]
  └─ returns FyiDocumentDto [L81]

