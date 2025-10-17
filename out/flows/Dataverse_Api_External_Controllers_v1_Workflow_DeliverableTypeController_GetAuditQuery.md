[web] GET /workflow/v1/deliverable-types/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.GetAuditQuery)  [L116–L121] status=200
  └─ maps_to EntityAuditDto [L119]
  └─ calls DeliverableTypeRepository.ReadQuery [L119]
  └─ query DeliverableType [L119]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DeliverableType writes=0 reads=1
    └─ mappings 1
      └─ EntityAuditDto

