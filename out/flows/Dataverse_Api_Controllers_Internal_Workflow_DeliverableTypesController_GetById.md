[web] GET /api/internal/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.GetById)  [L65–L73] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DeliverableTypeDto [L68]
    └─ automapper.registration ExternalApiMappingProfile (DeliverableType->DeliverableTypeDto) [L137]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeDto) [L358]
  └─ calls DeliverableTypeRepository.ReadQuery [L68]
  └─ query DeliverableType [L68]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L68]
      └─ ... (no implementation details available)

