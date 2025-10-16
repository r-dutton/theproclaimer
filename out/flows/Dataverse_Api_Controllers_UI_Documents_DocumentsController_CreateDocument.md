[web] POST /api/ui/documents/documents/  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CreateDocument)  [L246–L253] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request CreateDocumentCommand [L250]
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.CreateDocumentCommandHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method CreateDocument [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.CreateDocument [L30-L166]
  └─ returns FyiDocumentDto [L250]

