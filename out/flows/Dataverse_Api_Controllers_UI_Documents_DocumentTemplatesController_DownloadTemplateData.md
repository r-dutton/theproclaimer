[web] GET /api/ui/documents/templates/{templateId:guid}/download  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.DownloadTemplateData)  [L282–L288] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CanIAuthorDocumentTemplatesQuery [L285]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAuthorDocumentTemplatesQueryHandler.Handle [L18–L40]
      └─ uses_service UserService
        └─ method GetUserAsync [L29]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
  └─ sends_request GetDocumentTemplateDataQuery [L287]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentTemplateDataQueryHandler.Handle [L30–L95]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L75]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentTemplate>
        └─ method ReadQuery [L50]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L63]
          └─ ... (no implementation details available)

