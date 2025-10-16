[web] GET /api/ui/documents/templates/  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.FindTemplates)  [L53–L87] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindDocumentTemplatesQuery [L69]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.FindDocumentTemplatesQueryHandler.Handle [L57–L235]
      └─ uses_service UserService
        └─ method GetUserAsync [L80]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service IControlledRepository<DocumentTemplate>
        └─ method ReadQuery [L79]
          └─ ... (no implementation details available)

