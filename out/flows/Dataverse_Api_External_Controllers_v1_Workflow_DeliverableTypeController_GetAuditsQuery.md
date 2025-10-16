[web] GET /workflow/v1/deliverable-types/audits  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.GetAuditsQuery)  [L102–L108] status=200
  └─ maps_to EntityAuditDto [L105]
  └─ calls DeliverableTypeRepository.ReadQuery [L105]
  └─ query DeliverableType [L105]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L105]
      └─ ... (no implementation details available)

