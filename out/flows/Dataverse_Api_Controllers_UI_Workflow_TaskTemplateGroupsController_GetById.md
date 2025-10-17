[web] GET /api/ui/workflow/task-template-groups/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.GetById)  [L45–L53] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TaskTemplateGroupDto [L48]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
    └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
  └─ calls TaskTemplateGroupRepository.ReadQuery [L48]
  └─ query TaskTemplateGroup [L48]
    └─ reads_from TaskTemplateGroups
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplateGroup writes=0 reads=1
    └─ mappings 1
      └─ TaskTemplateGroupDto

