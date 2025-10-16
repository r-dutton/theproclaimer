[web] DELETE /api/ui/documents/templates/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.DeleteTemplate)  [L248–L254] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request CanIAuthorDocumentTemplatesQuery [L251]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAuthorDocumentTemplatesQueryHandler.Handle [L18–L40]
      └─ uses_service UserService
        └─ method GetUserAsync [L29]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

