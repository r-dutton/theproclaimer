[web] DELETE /api/internal/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.Delete)  [L115–L125] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L118]
  └─ write DeliverableType [L118]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method WriteQuery [L118]
      └─ ... (no implementation details available)

