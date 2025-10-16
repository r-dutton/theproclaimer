[web] POST /api/ui/documents/documents/duplicate-check  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.DuplicateCheck)  [L373–L428] status=201 [auth=Authentication.UserPolicy]
  └─ calls DocumentRepository.ReadQuery [L403]
  └─ query Document [L403]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L403]
      └─ ... (no implementation details available)

