[web] GET /workflow/v1/deliverables/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.Get)  [L52–L57]
  └─ maps_to DeliverableDto [L55]
    └─ automapper.registration ExternalApiMappingProfile (Deliverable->DeliverableDto) [L130]
    └─ automapper.registration DataverseMappingProfile (Deliverable->DeliverableDto) [L352]
  └─ calls DeliverableRepository.ReadQuery [L55]
  └─ queries Deliverable [L55]
    └─ reads_from Deliverables
  └─ uses_service IControlledRepository<Deliverable>
    └─ method ReadQuery [L55]

