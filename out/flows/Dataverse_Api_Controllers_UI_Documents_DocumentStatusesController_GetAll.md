[web] GET /api/ui/documents/statuses  (Dataverse.Api.Controllers.UI.Documents.DocumentStatusesController.GetAll)  [L33–L42] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentStatusDto [L36]
    └─ automapper.registration DataverseMappingProfile (DocumentStatus->DocumentStatusDto) [L415]
  └─ calls DocumentStatusRepository.ReadQuery [L36]
  └─ query DocumentStatus [L36]
    └─ reads_from DVS_DocumentStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentStatus writes=0 reads=1
    └─ mappings 1
      └─ DocumentStatusDto

