[web] DELETE /api/ui/workflow/task-templates/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.Delete)  [L101–L111] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaskTemplateRepository (methods: Remove,WriteQuery) [L108]
  └─ delete TaskTemplate [L108]
    └─ reads_from TaskTemplates
  └─ write TaskTemplate [L104]
    └─ reads_from TaskTemplates
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ TaskTemplate writes=2 reads=0

