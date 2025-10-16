[web] GET /workflow/v1/deliverable-types/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.FullQuery)  [L88–L95] status=200
  └─ calls DeliverableTypeRepository.ReadQuery [L92]
  └─ query DeliverableType [L92]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DeliverableType writes=0 reads=1

