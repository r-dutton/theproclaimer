[web] DELETE /api/ui/workflow/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.Delete)  [L76–L86] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaskTemplateGroupRepository.Remove [L83]
  └─ calls TaskTemplateGroupRepository.WriteQuery [L79]
  └─ writes_to TaskTemplateGroup [L83]
    └─ reads_from TaskTemplateGroups
  └─ writes_to TaskTemplateGroup [L79]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method WriteQuery [L79]

