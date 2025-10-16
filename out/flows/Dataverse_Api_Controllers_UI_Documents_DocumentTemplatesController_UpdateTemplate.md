[web] PUT /api/ui/documents/templates/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentTemplatesController.UpdateTemplate)  [L192–L198] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CanIAuthorDocumentTemplatesQuery [L195]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAuthorDocumentTemplatesQueryHandler.Handle [L18–L40]
      └─ uses_service UserService
        └─ method GetUserAsync [L29]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

