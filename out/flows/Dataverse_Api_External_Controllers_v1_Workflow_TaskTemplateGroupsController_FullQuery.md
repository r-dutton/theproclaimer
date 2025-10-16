[web] GET /workflow/v1/task-template-groups/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.FullQuery)  [L85–L92] status=200
  └─ calls TaskTemplateGroupRepository.ReadQuery [L89]
  └─ query TaskTemplateGroup [L89]
    └─ reads_from TaskTemplateGroups
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplateGroup writes=0 reads=1

