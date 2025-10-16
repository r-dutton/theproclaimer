[web] GET /workflow/v1/deliverables/  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.MinimalQuery)  [L69–L77] status=200
  └─ calls DeliverableRepository.ReadQuery [L73]
  └─ query Deliverable [L73]
    └─ reads_from Deliverables
  └─ uses_service IControlledRepository<Deliverable>
    └─ method ReadQuery [L73]
      └─ ... (no implementation details available)

