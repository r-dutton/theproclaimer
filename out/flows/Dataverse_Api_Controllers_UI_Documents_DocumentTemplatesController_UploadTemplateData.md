[web] POST /api/ui/documents/templates/{templateId:guid}/upload  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.UploadTemplateData)  [L290–L296] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request CanIAuthorDocumentTemplatesQuery [L293]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAuthorDocumentTemplatesQueryHandler.Handle [L18–L40]
      └─ uses_service UserService
        └─ method GetUserAsync [L29]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

