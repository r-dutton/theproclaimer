[web] GET /workflow/v1/task-template-groups/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.Get)  [L48–L54] status=200
  └─ maps_to TaskTemplateGroupDto [L51]
    └─ automapper.registration DataverseMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L373]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplateGroup->TaskTemplateGroupDto) [L155]
  └─ calls TaskTemplateGroupRepository.ReadQuery [L51]
  └─ query TaskTemplateGroup [L51]
    └─ reads_from TaskTemplateGroups
  └─ uses_service IControlledRepository<TaskTemplateGroup>
    └─ method ReadQuery [L51]
      └─ ... (no implementation details available)

