[web] GET /api/ui/documents/tags/  (Dataverse.Api.Controllers.UI.Documents.DocumentTagsController.Find)  [L26–L37] [auth=Authentication.UserPolicy]
  └─ calls DocumentTagRepository.ReadQuery [L33]
  └─ queries DocumentTag [L33]
    └─ reads_from DocumentTags
  └─ uses_service IControlledRepository<DocumentTag>
    └─ method ReadQuery [L33]

