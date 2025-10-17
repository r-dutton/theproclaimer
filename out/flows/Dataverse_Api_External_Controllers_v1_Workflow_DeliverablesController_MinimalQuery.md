[web] GET /workflow/v1/deliverables/  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.MinimalQuery)  [L69–L77] status=200
  └─ calls DeliverableRepository.ReadQuery [L73]
  └─ query Deliverable [L73]
    └─ reads_from Deliverables
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Deliverable writes=0 reads=1

