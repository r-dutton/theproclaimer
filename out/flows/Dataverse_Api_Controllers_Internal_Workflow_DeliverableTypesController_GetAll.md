[web] GET /api/internal/deliverable-types  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.GetAll)  [L53–L63] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DeliverableTypeDto [L56]
    └─ automapper.registration ExternalApiMappingProfile (DeliverableType->DeliverableTypeDto) [L137]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeDto) [L357]
  └─ calls DeliverableTypeRepository.ReadQuery [L56]
  └─ queries DeliverableType [L56]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L56]

