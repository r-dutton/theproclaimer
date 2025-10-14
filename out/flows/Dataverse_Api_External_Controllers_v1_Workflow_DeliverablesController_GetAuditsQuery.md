[web] GET /workflow/v1/deliverables/audits  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.GetAuditsQuery)  [L104–L110]
  └─ maps_to EntityAuditDto [L107]
  └─ calls DeliverableRepository.ReadQuery [L107]
  └─ queries Deliverable [L107]
    └─ reads_from Deliverables
  └─ uses_service IControlledRepository<Deliverable>
    └─ method ReadQuery [L107]

