[web] GET /workflow/v1/deliverable-types/audits  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.GetAuditsQuery)  [L102–L108] status=200
  └─ maps_to EntityAuditDto [L105]
  └─ calls DeliverableTypeRepository.ReadQuery [L105]
  └─ query DeliverableType [L105]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DeliverableType writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

