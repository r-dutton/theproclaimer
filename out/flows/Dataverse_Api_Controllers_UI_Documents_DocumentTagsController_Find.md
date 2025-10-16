[web] GET /api/ui/documents/tags/  (Dataverse.Api.Controllers.UI.Documents.DocumentTagsController.Find)  [L26–L37] status=200 [auth=Authentication.UserPolicy]
  └─ calls DocumentTagRepository.ReadQuery [L33]
  └─ query DocumentTag [L33]
    └─ reads_from DocumentTags
  └─ uses_service IControlledRepository<DocumentTag>
    └─ method ReadQuery [L33]
      └─ ... (no implementation details available)

