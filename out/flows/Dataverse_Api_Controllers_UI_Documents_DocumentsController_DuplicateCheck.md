[web] POST /api/ui/documents/documents/duplicate-check  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.DuplicateCheck)  [L373–L428] [auth=Authentication.UserPolicy]
  └─ calls DocumentRepository.ReadQuery [L403]
  └─ queries Document [L403]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L403]

