[web] PUT /api/internal/deliverable-types/{id:Guid}/reorder  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.Reorder)  [L102–L113] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L105]
  └─ writes_to DeliverableType [L105]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method WriteQuery [L105]

