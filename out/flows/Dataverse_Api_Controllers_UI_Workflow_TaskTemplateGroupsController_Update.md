[web] PUT /api/ui/workflow/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.Update)  [L64–L74] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaskTemplateGroupRepository.WriteQuery [L67]
  └─ writes_to TaskTemplateGroup [L67]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method WriteQuery [L67]

