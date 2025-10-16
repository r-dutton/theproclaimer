[web] DELETE /api/ui/documents/templates/{templateId:guid}/versions/{templateDocumentVersionId:guid}/field-groups/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.DeleteTemplateFieldGroup)  [L269–L280] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CanIAuthorDocumentTemplatesQuery [L275]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAuthorDocumentTemplatesQueryHandler.Handle [L18–L40]
      └─ uses_service UserService
        └─ method GetUserAsync [L29]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

