[web] GET /workflow/v1/deliverables/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.FullQuery)  [L89–L97] status=200
  └─ calls DeliverableRepository.ReadQuery [L93]
  └─ query Deliverable [L93]
    └─ reads_from Deliverables
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Deliverable writes=0 reads=1

