[web] GET /workflow/v1/deliverables/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.GetAuditQuery)  [L118–L123] status=200
  └─ maps_to EntityAuditDto [L121]
  └─ calls DeliverableRepository.ReadQuery [L121]
  └─ query Deliverable [L121]
    └─ reads_from Deliverables
  └─ uses_service IControlledRepository<Deliverable>
    └─ method ReadQuery [L121]
      └─ ... (no implementation details available)

