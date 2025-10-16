[web] GET /api/ui/documents/statuses/list  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetAllLight)  [L44–L53] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentStatusLightDto [L47]
    └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusLightDto) [L413]
  └─ calls DocumentStatusRepository.ReadQuery [L47]
  └─ query DocumentStatus [L47]
    └─ reads_from DVS_DocumentStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentStatus writes=0 reads=1
    └─ mappings 1
      └─ DocumentStatusLightDto

