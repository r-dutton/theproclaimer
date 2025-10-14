[web] GET /api/ui/workflow/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.GetById)  [L45–L53] [auth=Authentication.UserPolicy]
  └─ maps_to TaskTemplateGroupDto [L48]
    └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L372]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
  └─ calls TaskTemplateGroupRepository.ReadQuery [L48]
  └─ queries TaskTemplateGroup [L48]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method ReadQuery [L48]

