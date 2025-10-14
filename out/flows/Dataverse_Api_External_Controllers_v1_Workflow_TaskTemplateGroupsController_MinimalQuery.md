[web] GET /workflow/v1/task-template-groups/  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.MinimalQuery)  [L66–L73]
  └─ calls TaskTemplateGroupRepository.ReadQuery [L70]
  └─ queries TaskTemplateGroup [L70]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method ReadQuery [L70]

