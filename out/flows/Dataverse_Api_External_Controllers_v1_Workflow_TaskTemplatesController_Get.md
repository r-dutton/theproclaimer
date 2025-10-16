[web] GET /workflow/v1/task-templates/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.Get)  [L49–L55] status=200
  └─ maps_to TaskTemplateDto [L52]
    └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
  └─ calls TaskTemplateRepository.ReadQuery [L52]
  └─ query TaskTemplate [L52]
    └─ reads_from TaskTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplate writes=0 reads=1
    └─ mappings 1
      └─ TaskTemplateDto

