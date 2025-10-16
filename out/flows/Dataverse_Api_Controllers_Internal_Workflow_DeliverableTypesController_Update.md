[web] PUT /api/internal/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.Update)  [L90–L100] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L93]
  └─ write DeliverableType [L93]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DeliverableType writes=1 reads=0

