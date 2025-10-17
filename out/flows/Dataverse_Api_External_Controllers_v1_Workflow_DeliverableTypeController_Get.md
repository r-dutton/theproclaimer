[web] GET /workflow/v1/deliverable-types/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.Get)  [L52–L57] status=200
  └─ maps_to DeliverableTypeDto [L55]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeDto) [L358]
    └─ automapper.registration ExternalApiMappingProfile (DeliverableType->DeliverableTypeDto) [L137]
  └─ calls DeliverableTypeRepository.ReadQuery [L55]
  └─ query DeliverableType [L55]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DeliverableType writes=0 reads=1
    └─ mappings 1
      └─ DeliverableTypeDto

