[web] GET /workflow/v1/deliverable-types/  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.MinimalQuery)  [L69–L76] status=200
  └─ calls DeliverableTypeRepository.ReadQuery [L73]
  └─ query DeliverableType [L73]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DeliverableType writes=0 reads=1

