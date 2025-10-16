[web] DELETE /api/ui/workflow/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.Delete)  [L76–L86] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaskTemplateGroupRepository (methods: Remove,WriteQuery) [L83]
  └─ delete TaskTemplateGroup [L83]
    └─ reads_from TaskTemplateGroups
  └─ write TaskTemplateGroup [L79]
    └─ reads_from TaskTemplateGroups
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ TaskTemplateGroup writes=2 reads=0

