[web] PUT /api/ui/workflow/task-templates/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.Update)  [L71–L81] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaskTemplateRepository.WriteQuery [L74]
  └─ writes_to TaskTemplate [L74]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method WriteQuery [L74]

