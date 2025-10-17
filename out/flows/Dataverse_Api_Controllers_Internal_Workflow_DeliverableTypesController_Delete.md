[web] DELETE /api/internal/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.Delete)  [L115–L125] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L118]
  └─ write DeliverableType [L118]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DeliverableType writes=1 reads=0

