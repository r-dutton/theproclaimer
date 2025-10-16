[web] GET /workflow/v1/deliverable-types/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.Get)  [L52–L57] status=200
  └─ maps_to DeliverableTypeDto [L55]
    └─ automapper.registration ExternalApiMappingProfile (DeliverableType->DeliverableTypeDto) [L137]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeDto) [L358]
  └─ calls DeliverableTypeRepository.ReadQuery [L55]
  └─ query DeliverableType [L55]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L55]
      └─ ... (no implementation details available)

