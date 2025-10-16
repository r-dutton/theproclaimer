[web] GET /workflow/v1/task-template-groups/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.Get)  [L48–L54] status=200
  └─ maps_to TaskTemplateGroupDto [L51]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
    └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
  └─ calls TaskTemplateGroupRepository.ReadQuery [L51]
  └─ query TaskTemplateGroup [L51]
    └─ reads_from TaskTemplateGroups
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplateGroup writes=0 reads=1
    └─ mappings 1
      └─ TaskTemplateGroupDto

