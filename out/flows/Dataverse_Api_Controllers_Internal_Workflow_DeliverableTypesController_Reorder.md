[web] PUT /api/internal/deliverable-types/{id:Guid}/reorder  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.Reorder)  [L102–L113] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L105]
  └─ write DeliverableType [L105]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DeliverableType writes=1 reads=0

