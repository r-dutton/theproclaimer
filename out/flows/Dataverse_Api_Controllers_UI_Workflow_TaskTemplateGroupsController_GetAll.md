[web] GET /api/ui/workflow/task-template-groups  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplateGroupsController.GetAll)  [L34–L43] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TaskTemplateGroupDto [L37]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
    └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
  └─ calls TaskTemplateGroupRepository.ReadQuery [L37]
  └─ query TaskTemplateGroup [L37]
    └─ reads_from TaskTemplateGroups
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplateGroup writes=0 reads=1
    └─ mappings 1
      └─ TaskTemplateGroupDto

