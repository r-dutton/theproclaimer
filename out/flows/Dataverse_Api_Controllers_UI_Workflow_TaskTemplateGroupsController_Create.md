[web] POST /api/ui/workflow/task-template-groups  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.Create)  [L55–L62] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to TaskTemplateGroupLightDto [L61]
  └─ calls TaskTemplateGroupRepository.Add [L59]
  └─ insert TaskTemplateGroup [L59]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method Add [L59]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L61]
      └─ ... (no implementation details available)

