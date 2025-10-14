[web] GET /workflow/v1/deliverable-types/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.GetAuditQuery)  [L116–L121]
  └─ maps_to EntityAuditDto [L119]
  └─ calls DeliverableTypeRepository.ReadQuery [L119]
  └─ queries DeliverableType [L119]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L119]

