[web] GET /workflow/v1/deliverables/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.FullQuery)  [L89–L97]
  └─ calls DeliverableRepository.ReadQuery [L93]
  └─ queries Deliverable [L93]
    └─ reads_from Deliverables
  └─ uses_service IControlledRepository<Deliverable>
    └─ method ReadQuery [L93]

