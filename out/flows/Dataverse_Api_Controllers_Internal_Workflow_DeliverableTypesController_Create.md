[web] POST /api/internal/deliverable-types  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.Create)  [L75–L88] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DeliverableTypeDto [L87]
  └─ calls DeliverableTypeRepository.Add [L85]
  └─ calls DeliverableTypeRepository.WriteQuery [L78]
  └─ writes_to DeliverableType [L85]
    └─ reads_from DeliverableTypes
  └─ writes_to DeliverableType [L78]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method WriteQuery [L78]
  └─ uses_service IMapper
    └─ method Map [L87]

