[web] GET /api/ui/documents/tags/  (Dataverse.Api.Controllers.UI.Documents.DocumentTagsController.Find)  [L26–L37] status=200 [auth=Authentication.UserPolicy]
  └─ calls DocumentTagRepository.ReadQuery [L33]
  └─ query DocumentTag [L33]
    └─ reads_from DocumentTags
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentTag writes=0 reads=1

