[web] POST /api/ui/documents/{documentId:guid}/versions/copy  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.CopyDocumentVersion)  [L75–L81] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L79]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)
  └─ sends_request CopyDocumentVersionCommand [L80]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.CopyDocumentVersionCommandHandler.Handle [L30–L124]
      └─ uses_service UserService
        └─ method GetUserId [L79]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L89]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L54]
          └─ ... (no implementation details available)

