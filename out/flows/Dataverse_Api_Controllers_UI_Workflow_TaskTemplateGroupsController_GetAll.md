[web] GET /api/ui/workflow/task-template-groups  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.GetAll)  [L34–L43] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TaskTemplateGroupDto [L37]
    └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
  └─ calls TaskTemplateGroupRepository.ReadQuery [L37]
  └─ query TaskTemplateGroup [L37]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method ReadQuery [L37]
      └─ ... (no implementation details available)

