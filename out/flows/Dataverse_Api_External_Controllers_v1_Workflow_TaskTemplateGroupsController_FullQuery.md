[web] GET /workflow/v1/task-template-groups/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.FullQuery)  [L85–L92]
  └─ calls TaskTemplateGroupRepository.ReadQuery [L89]
  └─ queries TaskTemplateGroup [L89]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method ReadQuery [L89]

