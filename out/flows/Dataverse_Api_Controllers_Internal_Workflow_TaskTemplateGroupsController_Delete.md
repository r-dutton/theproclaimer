[web] DELETE /api/internal/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplateGroupsController.Delete)  [L76–L86] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TaskTemplateGroupRepository.Remove [L83]
  └─ calls TaskTemplateGroupRepository.WriteQuery [L79]
  └─ delete TaskTemplateGroup [L83]
    └─ reads_from TaskTemplateGroups
  └─ write TaskTemplateGroup [L79]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method WriteQuery [L79]
      └─ ... (no implementation details available)

