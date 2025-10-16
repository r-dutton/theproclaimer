[web] POST /api/internal/deliverable-types  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.Create)  [L75–L88] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DeliverableTypeDto [L87]
  └─ calls DeliverableTypeRepository (methods: Add,WriteQuery) [L85]
  └─ insert DeliverableType [L85]
    └─ reads_from DeliverableTypes
  └─ write DeliverableType [L78]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ DeliverableType writes=2 reads=0
    └─ mappings 1
      └─ DeliverableTypeDto

