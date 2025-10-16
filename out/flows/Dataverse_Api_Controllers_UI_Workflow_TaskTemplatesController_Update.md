[web] PUT /api/ui/workflow/task-templates/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.Update)  [L71–L81] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaskTemplateRepository.WriteQuery [L74]
  └─ write TaskTemplate [L74]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method WriteQuery [L74]
      └─ ... (no implementation details available)

