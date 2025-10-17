[web] GET /workflow/v1/deliverables/audits  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.GetAuditsQuery)  [L104–L110] status=200
  └─ maps_to EntityAuditDto [L107]
  └─ calls DeliverableRepository.ReadQuery [L107]
  └─ query Deliverable [L107]
    └─ reads_from Deliverables
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Deliverable writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

