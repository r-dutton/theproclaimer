[web] PUT /api/internal/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplateGroupsController.Update)  [L64–L74] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TaskTemplateGroupRepository.WriteQuery [L67]
  └─ write TaskTemplateGroup [L67]
    └─ reads_from TaskTemplateGroups
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ TaskTemplateGroup writes=1 reads=0

