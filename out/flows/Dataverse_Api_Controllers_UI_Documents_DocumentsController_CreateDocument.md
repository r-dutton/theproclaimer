[web] POST /api/ui/documents/documents/  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CreateDocument)  [L246–L253] [auth=Authentication.UserPolicy]
  └─ sends_request CreateDocumentCommand [L250]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.CreateDocumentCommandHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method CreateDocument [L29]
  └─ returns FyiDocumentDto [L250]

