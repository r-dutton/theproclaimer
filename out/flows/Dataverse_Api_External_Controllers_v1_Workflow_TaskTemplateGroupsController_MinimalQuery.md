[web] GET /workflow/v1/task-template-groups/  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.MinimalQuery)  [L66–L73] status=200
  └─ calls TaskTemplateGroupRepository.ReadQuery [L70]
  └─ query TaskTemplateGroup [L70]
    └─ reads_from TaskTemplateGroups
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplateGroup writes=0 reads=1

