[web] PUT /api/ui/workflow/task-templates/{id:Guid}/reorder  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.Reorder)  [L83–L99] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaskTemplateRepository.WriteQuery [L86]
  └─ write TaskTemplate [L86]
    └─ reads_from TaskTemplates
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ TaskTemplate writes=1 reads=0

