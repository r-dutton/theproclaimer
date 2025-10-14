[web] POST /api/internal/task-template-groups  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplateGroupsController.Create)  [L55–L62] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TaskTemplateGroupLightDto [L61]
  └─ calls TaskTemplateGroupRepository.Add [L59]
  └─ writes_to TaskTemplateGroup [L59]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method Add [L59]
  └─ uses_service IMapper
    └─ method Map [L61]

