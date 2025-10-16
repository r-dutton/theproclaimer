[web] GET /workflow/v1/task-template-groups/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.FullQuery)  [L85–L92] status=200
  └─ calls TaskTemplateGroupRepository.ReadQuery [L89]
  └─ query TaskTemplateGroup [L89]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method ReadQuery [L89]
      └─ ... (no implementation details available)

