[web] GET /api/ui/documents/statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetById)  [L67–L75] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentStatusDto [L70]
    └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L415]
  └─ calls DocumentStatusRepository.ReadQuery [L70]
  └─ query DocumentStatus [L70]
    └─ reads_from DVS_DocumentStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentStatus writes=0 reads=1
    └─ mappings 1
      └─ DocumentStatusDto

