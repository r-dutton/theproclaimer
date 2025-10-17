[web] GET /workflow/v1/deliverables/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.Get)  [L52–L57] status=200
  └─ maps_to DeliverableDto [L55]
    └─ automapper.registration ExternalApiMappingProfile (Deliverable->DeliverableDto) [L130]
    └─ automapper.registration DataverseMappingProfile (Deliverable->DeliverableDto) [L353]
  └─ calls DeliverableRepository.ReadQuery [L55]
  └─ query Deliverable [L55]
    └─ reads_from Deliverables
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Deliverable writes=0 reads=1
    └─ mappings 1
      └─ DeliverableDto

