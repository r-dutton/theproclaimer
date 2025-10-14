[web] GET /api/ui/documents/templates/  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.FindTemplates)  [L47–L81] [auth=Authentication.UserPolicy]
  └─ sends_request FindDocumentTemplatesQuery [L63]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.FindDocumentTemplatesQueryHandler.Handle [L57–L235]
      └─ uses_service IControlledRepository<DocumentTemplate>
        └─ method ReadQuery [L79]
      └─ uses_service UserService
        └─ method GetUserAsync [L80]

