[web] GET /workflow/v1/deliverable-types/  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.MinimalQuery)  [L69–L76] status=200
  └─ calls DeliverableTypeRepository.ReadQuery [L73]
  └─ query DeliverableType [L73]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L73]
      └─ ... (no implementation details available)

