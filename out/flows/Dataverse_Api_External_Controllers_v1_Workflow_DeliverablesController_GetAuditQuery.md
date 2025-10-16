[web] GET /workflow/v1/deliverables/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.GetAuditQuery)  [L118–L123] status=200
  └─ maps_to EntityAuditDto [L121]
  └─ calls DeliverableRepository.ReadQuery [L121]
  └─ query Deliverable [L121]
    └─ reads_from Deliverables
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Deliverable writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

