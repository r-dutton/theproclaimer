[web] DELETE /api/ui/workflow/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.Delete)  [L76–L86] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaskTemplateGroupRepository.Remove [L83]
  └─ calls TaskTemplateGroupRepository.WriteQuery [L79]
  └─ delete TaskTemplateGroup [L83]
    └─ reads_from TaskTemplateGroups
  └─ write TaskTemplateGroup [L79]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method WriteQuery [L79]
      └─ ... (no implementation details available)

