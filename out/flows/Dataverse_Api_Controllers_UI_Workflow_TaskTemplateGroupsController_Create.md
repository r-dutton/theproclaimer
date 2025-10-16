[web] POST /api/ui/workflow/task-template-groups  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.Create)  [L55–L62] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to TaskTemplateGroupLightDto [L61]
  └─ calls TaskTemplateGroupRepository.Add [L59]
  └─ insert TaskTemplateGroup [L59]
    └─ reads_from TaskTemplateGroups
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ TaskTemplateGroup writes=1 reads=0
    └─ mappings 1
      └─ TaskTemplateGroupLightDto

