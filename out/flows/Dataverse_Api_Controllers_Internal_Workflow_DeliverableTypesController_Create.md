[web] POST /api/internal/deliverable-types  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.Create)  [L75–L88] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DeliverableTypeDto [L87]
  └─ calls DeliverableTypeRepository.Add [L85]
  └─ calls DeliverableTypeRepository.WriteQuery [L78]
  └─ insert DeliverableType [L85]
    └─ reads_from DeliverableTypes
  └─ write DeliverableType [L78]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method WriteQuery [L78]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L87]
      └─ ... (no implementation details available)

