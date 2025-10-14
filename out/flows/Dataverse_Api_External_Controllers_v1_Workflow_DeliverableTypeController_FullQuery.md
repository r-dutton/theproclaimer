[web] GET /workflow/v1/deliverable-types/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.FullQuery)  [L88–L95]
  └─ calls DeliverableTypeRepository.ReadQuery [L92]
  └─ queries DeliverableType [L92]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L92]

